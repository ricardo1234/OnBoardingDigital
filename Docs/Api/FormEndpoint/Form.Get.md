# Get Form

## Url

    GET /api/form/{id}

## Status Codes

- 200 - OK
- 404 - Not Found

## Success Response

```json
  {
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "string",
    "sections": [
      {
        "id": "00000000-0000-0000-0000-000000000000",
        "order": 0,
        "name": "string",
        "canRepeat": false,
        "fields": [
          {
            "id": "00000000-0000-0000-0000-000000000000",
            "order": 1,
            "required": true,
            "description": "",
            "type": {
              "id": 0
            },
            "informationSettings": {
              "htmlValue": ""
            },
            "fileSettings": {
              "extensions": [
                "string"
              ]
            },
            "choiceSettings": {
              "group": "string"
            },
            "textSettings": {
              "charMaximum": 100,
              "charMinimum": 3,
              "validationExpression": "string"
            },
            "dateTimeSettings": {
              "hasTime": false,
              "hasDate": false,
              "isMinimumToday": false,
              "isMaximumToday": false
            }
          }
        ]
      },
    ]
  }
```
