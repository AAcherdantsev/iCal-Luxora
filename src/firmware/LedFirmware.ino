//||================================================================||\\
//||====== Smart Led Lamp "iCal Dolbit Normalno" with sound ========||\\
//||================================================================||\\
//||= The project is inspired by the work of the following people: =||\\
//||----------------------------------------------------------------||\\
//||===== AlexGyver, Nich1con, MishanyaTS, alvikskor, gunner47 =====||\\
//||= sutaburosu, SottNick, Stefan Petrick, SlingMaster, Palpalych =||\\
//||============== Daniel Wilson, Jason Coon, Stepko ===============||\\
//||================================================================||\\

#include <map>
#include <vector>
#include <math.h>
#include <Audio.h> // for sound from sd-card. Only 2-cores esp32 supported ( https://github.com/schreibfaul1/ESP32-audioI2S )
#include <FastLED.h>
#include <EncButton.h>

#include <microDS18B20.h>
#include <TM1637Display.h>

#include "FS.h"
#include "SD.h"
#include "SPI.h"
#include "SPIFFS.h"

#include "BluetoothSerial.h"

#include "BluetoothA2DPSink.h"

#include "Constants.h"

//#include <WiFi.h>
//#include <ESPmDNS.h>
//#include <WiFiUdp.h>
//#include <ArduinoOTA.h>
//#include <PubSubClient.h>
//#include <microDS18B20.h>
//#include <SimpleFTPServer.h>

#pragma region Enums


enum CommunicationMode : uint8_t
{
  WIFI_CLIENT, 
  WIFI_SERVER, 
  BLUETOOTH_SERIAL,
  A2DP
};

enum PowerMode : uint8_t
{
  SLILENT,
  NORMAL,
  EXTRIME
};

enum Language : uint8_t
{
  ENGLISH,
  RUSSIAN
};

enum EffectType : uint8_t
{
  REGULAR,
  SOUND_REACTION,
  CUSTOM,
  GAME
};

enum EffectParameterType : uint8_t
{
                // parameters:
  RUNNING_TEXT, // string
  COLOR_SLIDER, // int8_t, 0 - 255
  INT_SLIDER,   // int8_t, 0 - 255
  SWITCHER,     // text_then_toggled, text_then_untoggled,
};

#pragma endregion


/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

template <typename T>
class EffectParameter
{
public:
  uint8_t order;

  EffectParameterType type;

  std::map<Language, String> texts;

  T defaultValue, currentValue;

  EffectParameter() { }

  EffectParameter(uint8_t order, EffectParameterType type,  std::map<Language, String> texts, T defaultValue, T currentValue)
    : type{type}, order{order}, texts{texts}, defaultValue{defaultValue}, currentValue{currentValue} { }

};

struct Settings
{
  uint8_t brightness, 
          currentMode, 
          displayBrightness, // 0-7
          loudness; // 0-21

  bool enableFavoriteModesAtStartup;
  bool isFavoriteModesEnabled;
  vector<uint8_t> favoriteModes;
  
};

/*
class LedLamp
{
public:

  Audio sound; // for sound from sd card
  BluetoothA2DPSink a2dpSpeaker; 
  BluetoothSerial serialBT;

  // microphone, fan, photoresistor

  //Matrix matrix;

  //TM1637Display display = TM1637Display(PIN_DISPLAY_CLK, PIN_DISPLAY_DIO);
  //MicroDS18B20<PIN_TEMPERATURE_SENSOR> temperatureSensor;

  //GButton touchButton(PIN_BUTTON, LOW_PULL, NORM_OPEN); 

  void setupButton()
  {
    //touchButton.setStepTimeout(BUTTON_STEP_TIMEOUT);
    //touchButton.setClickTimeout(BUTTON_CLICK_TIMEOUT);
    //touchButton.setDebounce(BUTTON_SET_DEBOUNCE);
  }

  void setupA2dp()
  {
    i2s_pin_config_t config = 
    {
      .bck_io_num = PIN_EXTERNAL_DAC_BCK,
      .ws_io_num = PIN_EXTERNAL_DAC_WS,
      .data_out_num = PIN_EXTERNAL_DAC_OUT,
      .data_in_num = I2S_PIN_NO_CHANGE
    };

    a2dpSpeaker.set_pin_config(config);
  }

  void setupDisplay()
  {
    display.clear();
    display.setBrightness(7); // set the brightness to 7 (0:dimmest, 7:brightest)
  }

  void setupSoundFromSD()
  {
    pinMode(PIN_SD_CS, OUTPUT);
    digitalWrite(PIN_SD_CS, HIGH);
    SPI.begin(PIN_SD_SPI_SCK, PIN_SD_SPI_MISO, PIN_SD_SPI_MOSI);
    Serial.begin(115200);

    if(!SD.begin(PIN_SD_CS))
    {
      Serial.println("Error communication to SD card!");
      while(true);
    }

    sound.setPinout(PIN_EXTERNAL_DAC_BCK, PIN_EXTERNAL_DAC_WS, PIN_EXTERNAL_DAC_OUT);
    sound.setVolume(2); // 0...21
    sound.connecttoFS(SD,"/home.mp3");
  }

  void tick()
  {
    sound.loop();
  }
};
*/

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////













struct TrackingObject
{
public:
  float posX, posY, speedX, speedY, shift;
  uint8_t hue, state;
  bool isShift;
};

struct LiquidLampData
{
public:
  float hot, spf;
  unsigned mx, sc, tr;
};




class Matrix
{
private:
  CRGB leds[NUMBER_OF_LEDS];

public:
  uint8_t enlargedObjectNum;  // используемое в эффекте количество объектов

  LiquidLampData  liquidLampData[ENLARGED_OBJECT_MAX_COUNT]; // move to effect class
  long        enlargedObjectTime[ENLARGED_OBJECT_MAX_COUNT];

  TrackingObject trackingObjects[TRACKING_OBJECT_MAX_COUNT];

  Matrix()
  {
    FastLED.addLeds<WS2812B, PIN_FIRST_LED> (leds, 0,                  LEDS_PER_STRIP);
    FastLED.addLeds<WS2812B, PIN_SECOND_LED>(leds, LEDS_PER_STRIP,     LEDS_PER_STRIP);
    FastLED.addLeds<WS2812B, PIN_THIRD_LED> (leds, 2 * LEDS_PER_STRIP, LEDS_PER_STRIP);
    FastLED.setCorrection(TypicalLEDStrip);
    FastLED.setBrightness(30);
  }

  void setPowerMode(PowerMode mode)
  {
    if (mode == SLILENT) FastLED.setMaxPowerInMilliWatts(POWER_IN_SILENT_MODE);
    if (mode == NORMAL)  FastLED.setMaxPowerInMilliWatts(POWER_IN_NORMAL_MODE);
    if (mode == EXTRIME) FastLED.setMaxPowerInMilliWatts(POWER_IN_EXTRIME_MODE);
  }

  uint16_t getPixelNumber(uint8_t x, uint8_t y)
  {
    return (uint16_t)(15 + (x % 2) + 32 * (x / 2) + ((x % 2) * 2 - 1) * (y % 16) + (y / 16) * 768);
  }

  void update() 
  {
    FastLED.show(); 
  }

  void clear()
  {
    FastLED.clear();
    update();
  }

  CRGB& operator()(uint8_t x, uint8_t y)
  { 
    return leds[getPixelNumber(x, y)];
  }

  CRGB getPixelColor(uint8_t x, uint8_t y)
  { 
    return leds[getPixelNumber(x, y)];
  }

  void fillAll(CRGB color)
  {
    for (int16_t i = 0; i < NUMBER_OF_LEDS; i++) leds[i] = color;
  }

  void blurScreen(fract8 blur_amount)
  {
    blur2d(leds, HORIZONTAL_COUNT_LEDS, VERTICAL_COUNT_LEDS, blur_amount);
  }

  void dimAll(uint8_t value) 
  {
    nscale8(leds, NUMBER_OF_LEDS, value);
  }

  void drawPixel(int8_t x, int8_t y, CRGB color) // setPixelColor
  {
    if (x < 0 || x > (HORIZONTAL_COUNT_LEDS - 1) || y < 0 || y > (VERTICAL_COUNT_LEDS - 1)) return;

    leds[getPixelNumber(x, y)] = color;
  }

  void drawLine(uint8_t x1, uint8_t y1, uint8_t x2, uint8_t y2, CRGB color)
  {
    int8_t deltaX = abs(x2 - x1),
           deltaY = abs(y2 - y1),
           signX = x1 < x2 ? 1 : -1,
           signY = y1 < y2 ? 1 : -1,
           error = deltaX - deltaY;

    drawPixel(x2, y2, color);

    while (x1 != x2 || y1 != y2) 
    {
      drawPixel(x1, y1, color);

      int8_t error2 = error * 2;

      if (error2 > -deltaY) 
      {
        error -= deltaY;
        x1 += signX;
      }

      if (error2 < deltaX) 
      {
        error += deltaX;
        y1 += signY;
      }
    }
  }

  void drawPixelF(float x, float y, CRGB color)
  {
    #define WU_WEIGHT(a,b) ((uint8_t) (((a)*(b)+(a)+(b))>>8))

    // extract the fractional parts and derive their inverses

    uint8_t xx = (x - (int)x) * 255, 
            yy = (y - (int)y) * 255, 
            ix = 255 - xx,
            iy = 255 - yy;

    // calculate the intensities for each affected pixel
    uint8_t wu[4] = 
    {
      WU_WEIGHT(ix, iy), 
      WU_WEIGHT(xx, iy),
      WU_WEIGHT(ix, yy), 
      WU_WEIGHT(xx, yy)
    };

    // multiply the intensities by the colour, and saturating-add them to the pixels

    for (uint8_t i = 0; i < 4; i++) 
    {
      int16_t xn = x + (i & 1), 
              yn = y + ((i >> 1) & 1);

      CRGB clr = getPixelColor(xn, yn);

      clr.r = qadd8(clr.r, (color.r * wu[i]) >> 8);
      clr.g = qadd8(clr.g, (color.g * wu[i]) >> 8);
      clr.b = qadd8(clr.b, (color.b * wu[i]) >> 8);

      drawPixel(xn, yn, clr);
    }
  }

  void drawLineF(float x1, float y1, float x2, float y2, CRGB color) 
  {
    float deltaX = fabs(x2 - x1),
          deltaY = fabs(y2 - y1),
          error = deltaX - deltaY,
          signX = x1 < x2 ? 0.5 : -0.5,
          signY = y1 < y2 ? 0.5 : -0.5;

    while (x1 != x2 || y1 != y2) 
    {
      if ((signX > 0 && x1 > x2 + signX) || (signX < 0 && x1 < x2 + signX)) break;
      if ((signY > 0 && y1 > y2 + signY) || (signY < 0 && y1 < y2 + signY)) break;

      drawPixelF(x1, y1, color);

      float error2 = error;

      if (error2 > -deltaY) 
      {
        error -= deltaY;
        x1 += signX;
      }

      if (error2 < deltaX) 
      {
        error += deltaX;
        y1 += signY;
      }
    }
  }


  void drawCircleF(float x0, float y0, float radius, CRGB color) 
  {
    float x = 0, 
          error = 0, 
          y = radius, 
          delta = 1. - 2. * radius;

    while (y >= 0) 
    {
      drawPixelF(fmod(x0 + x + HORIZONTAL_COUNT_LEDS, HORIZONTAL_COUNT_LEDS), y0 + y, color);
      drawPixelF(fmod(x0 + x + HORIZONTAL_COUNT_LEDS, HORIZONTAL_COUNT_LEDS), y0 - y, color);
      drawPixelF(fmod(x0 - x + HORIZONTAL_COUNT_LEDS, HORIZONTAL_COUNT_LEDS), y0 + y, color);
      drawPixelF(fmod(x0 - x + HORIZONTAL_COUNT_LEDS, HORIZONTAL_COUNT_LEDS), y0 - y, color);

      error = 2. * (delta + y) - 1.;

      if (delta < 0 && error <= 0) 
      {
        x++;
        delta += 2. * x + 1.;
        continue;
      }

      error = 2. * (delta - x) - 1.;

      if (delta > 0 && error > 0) 
      {
        y--;
        delta += 1. - 2. * y;
        continue;
      }
      x++;
      delta += 2. * (x - y);
      y--;
    }
  }
};

// if (millis() - effTimer >= (256U + shift - coef * modes[currentMode].Speed))
// min(MAX_DELAY_FOR_EFFECT, max(MIN_DELAY_FOR_EFFECT, 256U + shift - coef * speed));
// delay =  shift + speed * coef;


class BaseEffect
{
public:
  String name;

  float speedCoef = 1;

  int8_t delayShift = 0;

  EffectType effectType;

  std::map<Language, String> effectNames;

  EffectParameter<uint8_t> brightness, speed;

  Matrix* matrix = nullptr;

  BaseEffect(Matrix* matrix) : matrix{matrix} 
  { 
    effectType = EffectType::REGULAR;

    brightness.order = 0;
    brightness.currentValue = 40;
    brightness.defaultValue = 40;
    brightness.type = EffectParameterType::INT_SLIDER;
    brightness.texts[ENGLISH] = "Brightness";
    brightness.texts[RUSSIAN] = "Яркость";

    speed.order = 1;
    speed.currentValue = 10;
    speed.defaultValue = 10;
    speed.type = EffectParameterType::INT_SLIDER;
    speed.texts[ENGLISH] = "Speed";
    speed.texts[RUSSIAN] = "Скорость";

  }

  virtual void tick();
};

class EffectWhiteColorStripe : BaseEffect
{
public:
  // две дополнительные единицы бегунка Масштаб на границе вертикального и 
  // горизонтального варианта эффекта (с каждой стороны границы) будут для света всеми светодиодами в полную силу
  // params:
  #define BORDERLAND 2 
  
  uint8_t scale = 30, speed = 30;

  EffectWhiteColorStripe(Matrix* matrix) : BaseEffect(matrix) 
  {
    effectNames[RUSSIAN] = "Белый цвет";
    effectNames[ENGLISH] = "White Color";
  }
  // ------------- ещё более белый свет (с вертикальным вариантом) -------------
  // ------------------------------(c) SottNick --------------------------------
  virtual void tick() // whiteColorStripeRoutine
  {
    matrix->clear();

    uint8_t thisSize = VERTICAL_COUNT_LEDS;
    uint8_t halfScale = scale;

    if (halfScale > 50U)
    {
      thisSize = HORIZONTAL_COUNT_LEDS;
      halfScale = 101U - halfScale;
    }

    halfScale = constrain(halfScale, 0U, 50U - BORDERLAND);

    uint8_t center = (uint8_t)round(thisSize / 2.0F) - 1U;
    uint8_t offset = (uint8_t)(!(thisSize & 0x01));

    uint8_t fullFill = center / (50.0 - BORDERLAND) * halfScale;
    uint8_t iPol = (center / (50.0 - BORDERLAND) * halfScale - fullFill) * 255;

    for (int16_t i = center; i >= 0; i--)
    {
      CRGB color = CHSV(45U, Arduino_h::map(speed, 0U, 255U, 0U, 170U), 
                        i > (center - fullFill - 1) ? 255U : iPol * (i > center - fullFill - 2)); 

      if (scale <= 50U)
      {
        for (uint8_t x = 0; x < HORIZONTAL_COUNT_LEDS; x++)
        {
          matrix->drawPixel(x, i, color);  // при чётной высоте матрицы максимально яркими отрисуются 2 центральных горизонтальных полосы
          matrix->drawPixel(x, VERTICAL_COUNT_LEDS + offset - i - 2U, color);  // при нечётной - одна, но дважды
        }
      }
      else
      {
        for (uint8_t y = 0; y < VERTICAL_COUNT_LEDS; y++)
        {
          matrix->drawPixel((i + speed - 1U) % HORIZONTAL_COUNT_LEDS, y, color); // при чётной ширине матрицы максимально яркими отрисуются 2 центральных вертикальных полосы
          matrix->drawPixel((HORIZONTAL_COUNT_LEDS + offset - i + speed - 3U) % HORIZONTAL_COUNT_LEDS, y, color); // при нечётной - одна, но дважды
        }
      }
    }
  }
};

Matrix matrix;
BluetoothA2DPSink a2dpSpeaker; 

void setup()
{
  i2s_pin_config_t pinConfig = 
    {
      .bck_io_num = PIN_EXTERNAL_DAC_BCK,
      .ws_io_num = PIN_EXTERNAL_DAC_WS,
      .data_out_num = PIN_EXTERNAL_DAC_OUT,
      .data_in_num = I2S_PIN_NO_CHANGE
    };

/*
  i2s_config_t soundConfig = 
  {
    .sample_rate = (uint32_t)44100,
    .bits_per_sample = I2S_BITS_PER_SAMPLE_24BIT
  };
*/

  a2dpSpeaker.set_pin_config(pinConfig);
  //a2dpSpeaker.set_i2s_config(soundConfig);

  a2dpSpeaker.start("MyMusic");
}

uint counter;

uint32_t myTimer1;


void loop() 
{

  if (millis() - myTimer1 >= 500) {   // таймер на 500 мс (2 раза в сек)
    myTimer1 = millis();              // сброс таймера
  
    for (int i = 0; i < VERTICAL_COUNT_LEDS; i++)
    {
      for (int j = 0; j < HORIZONTAL_COUNT_LEDS; j++)
      {
        matrix(i, j) = CHSV(i + j + counter, 255, 255);
      }
    }
  
    matrix.update();
  }

  //delay(500);         // скорость движения радуги
}