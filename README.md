# Изучено в процессе разработки
- JWT авторизаия
- Основы EF Core
- Основы "Чистой архитектуры"

# Endpoints

## Log in endpoints

### Авторизация сотрудника:

Возвращает информацию о доступах к интерфейсам, доступным для управления складам и JWT-token для авторизации. Устанавливает Refresh token в cookie.

    Method: POST 
    Route: /api/login

Пример тела запроса:

```json
{
  "password": 0 // int, не может быть null
}
```

Пример тела ответа:

```json
{
    "accesses": {
        "employeeCard": true,
        "positionDirectory": false,
        "changes": false,
        "visitSchedule": true,
        "accounting": false
    },
    "stocks": [
        {
            "stockId": 5,
            "stockName": "Аша"
        }
    ],
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJFbXBsb3llZUlkIjoiMzMyODRhMzctZjk5OC00OWJlLWFiZjEtMWY1MzMyNzczNTEyIiwiZXhwIjoxNjc1MDY5NzA2LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjgwODAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjgwODAifQ.k-00FJVVZKfxqblfAkComrz0HCAror-Nhdho6neVd8w"
}
```

### Запрос на обновление JWT токена

Возвращает новый токен, если refresh token валиден. Если нет -- необходимо снова авторизовываться.

```
Method: POST
Route: /api/LogIn/refresh-token
```

Пример тела запроса:

```json
"string with old JWT"
```

Пример тела ответа:

```json
"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJFbXBsb3llZUlkIjoiMzMyODRhMzctZjk5OC00OWJlLWFiZjEtMWY1MzMyNzczNTEyIiwiZXhwIjoxNjc1MDY3MjY3LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjgwODAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjgwODAifQ.JAkkoF8dSb4rBKsOvD9_IgsrCgKWXAp4znfhagLnQ1c" // строка с новым токеном
```

## Employee endpoints

### Полуение данных о сотруднике

Возвращает данные о конкретном сотруднике по UUID

```
Method: GET
Route: /api/Employee/{EmployeeId}
```

Пример тела ответа:

```json
{
    "employeeId": "d4899099-2a38-466e-af94-37f84475969a",
    "name": "Leanna",
    "password": 817227,
    "surname": "Oberbrunner",
    "patronymic": "Brycenson",
    "birthday": "1985-02-10",
    "passportNumber": "218912414",
    "passportIssuer": "89qcur67tp80plz2q3ku",
    "passportIssueDate": "2013-10-01",
    "startOfTotalSeniority": "2013-10-01",
    "startOfLuchSeniority": "2013-10-01",
    "dateOfTermination": null,
    "position": {
        "positionId": "44213af5-ab16-4853-9d8e-c2d6c176ef7f",
        "name": "Администратор",
        "salary": 50000.00,
        "quarterlyBonus": 25000.00,
        "interfaceAccesses": {
            "employeeCard": true,
            "positionDirectory": true,
            "changes": true,
            "visitSchedule": true,
            "accounting": true
        }
    },
    "link": "680",
    "stocks": [
        {
            "stockId": 4,
            "stockName": "Асбест"
        },
        {
            "stockId": 5,
            "stockName": "Аша"
        },
        {
            "stockId": 6,
            "stockName": "Белорецк"
        },
        {
            "stockId": 7,
            "stockName": "Березники"
        }
    ],
    "forkliftControl": true,
    "rolleyesControl": true,
    "salary": 50000.00,
    "percentageOfSalaryInAdvance": 33.0,
    "dateOfStartInTheCurrentPosition": "2023-01-25",
    "dateOfStartInTheCurrentStock": "2023-01-25",
    "dateOfStartInTheCurrentLink": "2023-01-25"
}
```

### Получение данных обо всех сотрудниках

Выдает список сотрудников с их кратким описанием и UUID.

```
Method: GET
Route: /api/Employee/
```

Пример тела ответа:

```json
[
    {
        "employeeId": "77538c6a-0b9a-4ce7-8db6-5c9ff2413d50",
        "name": "Андрей",
        "surname": "Бабин",
        "patronymic": "Спартакович",
        "stocks": [
            {
                "stockId": 6,
                "stockName": "Белорецк"
            }
        ],
        "link": "6.1 Ночное"
    },
    {
        "employeeId": "f79e5a74-8465-423b-b921-0a9dd24afd18",
        "name": "Chelsie",
        "surname": "Johnson",
        "patronymic": "Estrellason",
        "stocks": [
            {
                "stockId": 6,
                "stockName": "Белорецк"
            }
        ],
        "link": "6.1 Дневное"
    }
]
```

### Регистрация сотрудника

```
Method: POST
Route: /api/Employee
```

Пример тела запроса:

```json
{
  "password": 0,
  "name": "string",
  "surname": "string",
  "patronymic": "string",
  "birthday": "2023-01-30T09:34:06.465Z",
  "passportNumber": "string",
  "passportIssuer": "string",
  "passportIssueDate": "2023-01-30T09:34:06.465Z",
  "startOfTotalSeniority": "2023-01-30T09:34:06.465Z",
  "startOfLuchSeniority": "2023-01-30T09:34:06.465Z",
  "dateOfTermination": "2023-01-30T09:34:06.465Z", // необязательное поле -> может быть null
  "positionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "salary": 0, // decimal поле
  "link": "6.1 Дневное", // необязательное поле -> может быть null
  "stock": "[6]", // Формат именно такой: json массив спаршеный в строку. Необязательное поле -> может быть null
  "forkliftControl": true,
  "rolleyesControl": true,
  "percentageOfSalaryInAdvance": 0, // от 0 до 100
  "dateOfStartInTheCurrentPosition": "2023-01-30T09:34:06.465Z", // необязательное поле -> может быть null
  "dateOfStartInTheCurrentStock": "2023-01-30T09:34:06.465Z", // необязательное поле -> может быть null
  "dateOfStartInTheCurrentLink": "2023-01-30T09:34:06.465Z" // необязательное поле -> может быть null
}
```


Прмер тела ответа:
```json
{
    "employeeId": "fd58823d-6b05-45f6-a989-0f3f248417b9",
    "password": 250612,
    "name": "Josue",
    "surname": "Abshire",
    "patronymic": "Kavonson",
    "birthday": "2023-01-30",
    "passportNumber": "563661404",
    "passportIssuer": "gbjd",
    "passportIssueDate": "2023-01-30",
    "startOfTotalSeniority": "2023-01-30",
    "startOfLuchSeniority": "2023-01-30",
    "dateOfTermination": null,
    "dateOfStartInTheCurrentPosition": "2023-01-30",
    "salary": 50.0,
    "quarterlyBonus": 5000.00,
    "percentageOfSalaryInAdvance": 75,
    "link": "6.1 Дневное",
    "dateOfStartInTheCurrentLink": "2023-01-30",
    "stocks": "[6]",
    "dateOfStartInTheCurrentStock": "2023-01-30",
    "forkliftControl": true,
    "rolleyesControl": true,
    "position": {
        "positionId": "d370e736-8466-437c-a739-121bf353c01a",
        "name": "Грузчик",
        "salary": 25000.00,
        "quarterlyBonus": 5000.00,
        "interfaceAccesses": "{\"Changes\": false, \"Accounting\": false, \"EmployeeCard\": false, \"VisitSchedule\": false, \"PositionDirectory\": false}" // Нужно распарсить в json
    } 
}
```

### Обновление информации о сотруднике

```
Method: PUT
Route: /api/Employee/{EmployeeId}
```

Пример тела запроса:
```json
{
    "name": "Mackenzie",
    "password": 654321,
    "surname": "Muller",
    "patronymic": "Addisonson",
    "birthday": "2007-03-01T19:00:00.000Z",
    "passportNumber": "123456",
    "passportIssuer": "4wy99tb2fibreze40ufj",
    "passportIssueDate": "05/21/2017",
    "startOfTotalSeniority": "2017-03-01T19:00:00.000Z",
    "startOfLuchSeniority": "2017-03-01T19:00:00.000Z",
    "dateOfTermination": null, // может быть null или датой
    "link": "5.2 Ночное",
    "stocks": "[5]",
    "forkliftControl": true,
    "rolleyesControl": true,
    "salary": 30000,
    "percentageOfSalaryInAdvance": 9,
    "dateOfStartInTheCurrentPosition": "2017-03-01T19:00:00.000Z", // может быть null
    "dateOfStartInTheCurrentStock": "2017-03-01T19:00:00.000Z", // может быть null
    "dateOfStartInTheCurrentLink": "2017-03-01T19:00:00.000Z", // может быть null
    "positionId": "d370e736-8466-437c-a739-121bf353c01a"
}
```

Пример тела овета: 

```json
{
    "employeeId": "fd58823d-6b05-45f6-a989-0f3f248417b9",
    "password": 654321,
    "name": "Mackenzie",
    "surname": "Muller",
    "patronymic": "Addisonson",
    "birthday": "2007-03-01",
    "passportNumber": "123456",
    "passportIssuer": "4wy99tb2fibreze40ufj",
    "passportIssueDate": "2017-05-21",
    "startOfTotalSeniority": "2017-03-01",
    "startOfLuchSeniority": "2017-03-01",
    "dateOfTermination": null,
    "dateOfStartInTheCurrentPosition": "2023-01-30",
    "salary": 30000.0,
    "quarterlyBonus": 5000.00,
    "percentageOfSalaryInAdvance": 9,
    "link": "5.2 Ночное",
    "dateOfStartInTheCurrentLink": "2023-01-30",
    "stocks": "[5]",
    "dateOfStartInTheCurrentStock": "2023-01-30",
    "forkliftControl": true,
    "rolleyesControl": true,
    "position": {
        "positionId": "d370e736-8466-437c-a739-121bf353c01a",
        "name": "Грузчик",
        "salary": 25000.00,
        "quarterlyBonus": 5000.00,
        "interfaceAccesses": "{\"Changes\": false, \"Accounting\": false, \"EmployeeCard\": false, \"VisitSchedule\": false, \"PositionDirectory\": false}"
    }
}
```

### Увольнение сотрудника

```
Method: DELETE
Route: /api/Employee/{EmployeeId}
```

Пример тела запроса:

```json
{
    "dateOfTermination": "2023-01-30T19:00:00.000Z"
}
```

## Position Endpoints

### Добавление должности
Добавление должности и распределение доступов для нее.

```
Method: POST
Route: /api/Position
```

Пример тела запроса:

```json
{
    "name": "zxc",
    "salary": 500000,
    "quarterlyBonus": 750000,
    "interfaceAccesses": {
        "employeeCard": true,
        "positionDirectory": false,
        "changes": false,
        "visitSchedule": true,
        "accounting": false
    }
}
```

### Получение информации о должности

```
Method: GET
Route: /api/Position/{PositionId}
```

Пример тела ответа:

```json
{
    "positionId": "eee84bfc-c358-4b2e-8cda-956c175793e6",
    "name": "zxc",
    "salary": 500000.00,
    "quarterlyBonus": 750000.00,
    "interfaceAccesses": {
        "employeeCard": true,
        "positionDirectory": false,
        "changes": false,
        "visitSchedule": true,
        "accounting": false
    }
}
```

### Получение списка должностей

```
Method: GET
Route: /api/Position/
```


Пример тела ответа:

```json
[
    {
        "positionId": "d370e736-8466-437c-a739-121bf353c01a",
        "name": "Грузчик"
    },
    {
        "positionId": "9ad29fb2-f9c4-4e4d-9155-12af0227ea67",
        "name": "Уволен"
    },
    {
        "positionId": "227cd892-3586-4993-acf8-8e1042a1b899",
        "name": "Кладовщик"
    },
    {
        "positionId": "eee84bfc-c358-4b2e-8cda-956c175793e6",
        "name": "zxc"
    }
]
```

### Обновить информацию о должности

```
Method: PUT
Route: /api/Position
```

Пример тела запроса:

```json
{
    "positionId": "9ad29fb2-f9c4-4e4d-9155-12af0227ea67",
    "name": "Уволен",
    "salary": 0.00,
    "quarterlyBonus": 0,
    "interfaceAccesses": "{\"Changes\": false, \"Accounting\": false, \"EmployeeCard\": false, \"VisitSchedule\": false, \"PositionDirectory\": false}"
}
```

## Stock endpoints

### Подгрузка складов с API

Подгружает список складов с главного API в БД этого проекта

```
Method: POST
Route: /api/Stock
```

### Добавление звена в склад

```
Method: POST
Route: /api/Stock/{stockId}
```

Пример тела запроса:

```json
{
    "name": "7.1 Дневное"
}
```

### Получение звений выбранного склада

```
Method: GET
Route: /api/stock/{stockId}
```

Пример тела ответа:

```json
[
    "7.1 Дневное",
    "7.1 Ночное",
    "7.2 Ночное",
    "7.3 Ночное"
]
```

## Shift Endpoints

### Открытие смены

Принимает сотрудников текущего склада, которые должны заступить на смену и время смены(Дневная/Ночная). Отдает UUID смены.

```
Method: POST
Route: /api/shift/open
```

Пимер тела запроса:

```json
{
    "stockId": 7,
    "employees": ["d25bb579-d1e9-462d-8eae-77d19fc748cf", "331e5d1d-0b2d-4254-9ade-f404aeee2e1d", "363c82d6-8243-4fe6-912d-0e18cd4f37c2"],
    "dayOrNight": "Дневная"
}
```

Пример тела ответа:

```json
"cf1c0578-c154-455b-a1f0-8f18e42a4efc"
```

### Закрытие смены

Принимает UUID смены, кол-во часов, отработанных сотрудником.

```
Method: POST
Route: /api/shift/close
```

Пример тела запроса:

```json
{
    "shiftId": "eda7d998-3f41-4d85-afe4-baec394ac790",
    "WorkedHours": {
        "d25bb579-d1e9-462d-8eae-77d19fc748cf": 1,
        "331e5d1d-0b2d-4254-9ade-f404aeee2e1d": 1,
        "363c82d6-8243-4fe6-912d-0e18cd4f37c2": 1,
    }
}
```

### Получение информации о наличии открытых смен на текущем складе

```
Method: GET
Route: /api/shift/get/{stockId}
```

Пример тела ответа:
```json
{
    "shiftId": "cf1c0578-c154-455b-a1f0-8f18e42a4efc",
    "dayOrNight": "Дневная",
    "employees": [
        {
            "employeeId": "d25bb579-d1e9-462d-8eae-77d19fc748cf",
            "fullName": "Wyman Kimberly Nicolasson"
        },
        {
            "employeeId": "331e5d1d-0b2d-4254-9ade-f404aeee2e1d",
            "fullName": "Shanahan Vernie Aaronson"
        },
        {
            "employeeId": "363c82d6-8243-4fe6-912d-0e18cd4f37c2",
            "fullName": "Kirlin Marcelino Mireyason"
        }
    ]
}
```

### Обновление инфорации о текущей смене 

Изменения о времени проведения смены и ее составе. 

```
Method: PUT
Route: /api/shift/{ShiftId}
```

Пример тела запроса:

```json
{
    "dayOrNight": "Дневная",
    "employees": [
        "d25bb579-d1e9-462d-8eae-77d19fc748cf",
        "331e5d1d-0b2d-4254-9ade-f404aeee2e1d",
        "363c82d6-8243-4fe6-912d-0e18cd4f37c2",
        "618dc037-b709-4eae-856e-c53b599c7e41"
    ]
}
```

### Обновление информации о прошедшей смене

Выставление штрафов и засылов

```
Method: PATCH
Route: /api/shift/{ShiftId}
```

Пример тела запроса:

```json
{
  "penalty": 250,
  "penaltyComment": "Что-то уронил",
  "send": 500,
  "sendComment": "какой-то засыл"
}
```

## Attendance Endpoints

### Получение посещаемости

Получение посещаемости склада за заданный период (месяц + год)

```
Method: GET
Route: /api/attendance
```

Пример тела запроса:
```json
{
    "stockid": 5,
    "month": 1,
    "year": 2023
}
```

Пример тела ответа:
```json
[
    {
        "employee": {
            "employeeId": "33284a37-f998-49be-abf1-1f5332773512",
            "fullName": "Thiel Aglae Ludwigson",
            "positionName": "Кладовщик"
        },
        "shifts": [
            {
                "shiftId": "751033c1-8e84-4087-ac1b-c2752386a02a",
                "employeeId": "33284a37-f998-49be-abf1-1f5332773512",
                "day": 24,
                "dayOrNight": "Дневная",
                "workedHours": 9,
                "penalty": null,
                "penaltyComment": null,
                "send": null,
                "sendComment": null
            },
            {
                "shiftId": "1fd65eae-d8ef-407a-b808-d49d624a2373",
                "employeeId": "33284a37-f998-49be-abf1-1f5332773512",
                "day": 25,
                "dayOrNight": "Дневная",
                "workedHours": 6,
                "penalty": null,
                "penaltyComment": null,
                "send": null,
                "sendComment": null
            }
        ]
    },
    {
        "employee": {
            "employeeId": "613335c9-0f39-4835-9f33-e5487591eeae",
            "fullName": "Ruecker Earline Annetteson",
            "positionName": "Грузчик"
        },
        "shifts": [
            {
                "shiftId": "76bcdff2-5eda-41c3-a572-fb98e4ec4da9",
                "employeeId": "613335c9-0f39-4835-9f33-e5487591eeae",
                "day": 24,
                "dayOrNight": "Дневная",
                "workedHours": 7,
                "penalty": null,
                "penaltyComment": null,
                "send": null,
                "sendComment": null
            },
            {
                "shiftId": "4ef6ffa4-13b8-4aab-a36b-890d58774984",
                "employeeId": "613335c9-0f39-4835-9f33-e5487591eeae",
                "day": 25,
                "dayOrNight": "Дневная",
                "workedHours": 6,
                "penalty": null,
                "penaltyComment": null,
                "send": null,
                "sendComment": null
            }
        ]
    }
]
```


## WorkPlan Endpoint

### Создание рабочего плана для сотрудника 

Распределение часов и смен сотрудника в текущем месяце

```
Method: POST
Rroute: /api/workplan/?employeeId=&year=&month=
```

Пример тела запроса:
```json
{
    "numberOfDayShifts": 12,
    "numberOfHoursPerDayShift": 3,
    "numberOfNightShifts": 0,
    "numberOfHoursPerNightShift": 0
}
```
