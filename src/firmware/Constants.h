#define FIRMWARE_VERSION "1.0.0"

// comment, if it's not supported

#define HAS_FAN
#define HAS_DISPLAY
#define HAS_SD_CARD
#define HAS_MICROPHONE
#define HAS_PHOTORESISTOR
#define HAS_SENSOR_BUTTON
#define HAS_TEMPERATURE_SENSOR

#define DEFAULT_A2DP_SPEAKER_NAME "LED Lamp Speaker"
#define DEFAULT_BLUE_SERIAL_NAME  "Bluetooth LED Lamp"

#define DEFAULT_WI_FI_SERVER_NAME "Wi-Fi Led Lamp (server)"
#define DEFAULT_WI_FI_SERVER_PASS "12345678"

#define DEFAULT_WI_FI_CLIENT_NAME "Wi-Fi Led Lamp (client)"
#define DEFAULT_WI_FI_CLIENT_PASS "12345678"

#define DEFAULT_FTP_SERVER_NAME "Lamp FTP-Server"
#define DEFAULT_FTP_SERVER_PASS "12345678"

#define DEFAULT_RUNNING_TEXT  "Default text"

#define DEFAULT_BRIGHTNESS         (40U)

#define LEDS_PER_STRIP            (768U)
#define VERTICAL_COUNT_LEDS        (48U)
#define HORIZONTAL_COUNT_LEDS      (48U)

#define POWER_IN_SILENT_MODE    (75000U)
#define POWER_IN_NORMAL_MODE   (180000U)
#define POWER_IN_EXTRIME_MODE  (320000U)

#define BUTTON_SET_DEBOUNCE           10
#define BUTTON_STEP_TIMEOUT          100
#define BUTTON_CLICK_TIMEOUT         500

#define MAX_DELAY_FOR_EFFECT     (1000U)
#define MIN_DELAY_FOR_EFFECT        (5U)

#define PIN_EXTERNAL_DAC_WS           25
#define PIN_EXTERNAL_DAC_BCK          26
#define PIN_EXTERNAL_DAC_OUT          22

#define PIN_SD_CS                      5
#define PIN_SD_SPI_SCK                18
#define PIN_SD_SPI_MOSI               23
#define PIN_SD_SPI_MISO               19

#define PIN_FIRST_LED                 13
#define PIN_SECOND_LED                16
#define PIN_THIRD_LED                  4

////////////////////////////////////////

#define PIN_DISPLAY_CLK               35 
#define PIN_DISPLAY_DIO               34

#define PIN_TEMPERATURE_SENSOR         8
#define PIN_PHOTORESISTOR              5
#define PIN_BUTTON 5
#define PIN_MICROPHONE                 7
#define PIN_PWM_FAN                    8
#define PIN_MUSIC                      8

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#define NUMBER_OF_LEDS HORIZONTAL_COUNT_LEDS * VERTICAL_COUNT_LEDS

//массивы состояния объектов, которые могут использоваться в любом эффекте
#define TRACKING_OBJECT_MAX_COUNT  (100U)                      // максимальное количество отслеживаемых объектов (очень влияет на расход памяти)
#define ENLARGED_OBJECT_MAX_COUNT  (HORIZONTAL_COUNT_LEDS * 2) // максимальное количество сложных отслеживаемых объектов (меньше, чем trackingOBJECT_MAX_COUNT)

//константы размера матрицы вычисляется только здесь и не меняется в эффектах
#define CENTER_X_MINOR (HORIZONTAL_COUNT_LEDS / 2) - ((HORIZONTAL_COUNT_LEDS - 1) & 0x01);  // центр матрицы по ИКСУ, сдвинутый в меньшую сторону, если ширина чётная
#define CENTER_Y_MINOR (VERTICAL_COUNT_LEDS / 2)   - ((VERTICAL_COUNT_LEDS - 1)   & 0x01);  // центр матрицы по ИГРЕКУ, сдвинутый в меньшую сторону, если высота чётная
#define CENTER_X_MAJOR HORIZONTAL_COUNT_LEDS / 2   + (HORIZONTAL_COUNT_LEDS % 2);           // центр матрицы по ИКСУ, сдвинутый в большую сторону, если ширина чётная
#define CENTER_Y_MAJOR VERTICAL_COUNT_LEDS / 2     + (VERTICAL_COUNT_LEDS % 2);             // центр матрицы по ИГРЕКУ, сдвинутый в большую сторону, если высота чётная

