# ASP .NET Core REST API

## Endpoints

## Usuario

* `GET /api/users`: Obtener todos los usuarios
* `GET /api/users/{id}`: Obtener un usuario específico
* `DELETE /api/users/{id}`: Eliminar un usuario específico
* `POST /api/users`: Crear un nuevo usuario, como por ejemplo :
  ```bash
   {
        "userName": "jose7",
        "name": "Jose",
        "lastName": "Garcia",
        "age": 25,
    }
  ```
    
* `PUT /api/users/{id}`: Editar un usuario, como por ejemplo :
```bash
   {
        "id": 1,
        "userName": "jose77",
        "name": "Jose",
        "lastName": "Garcia",
        "age": 25,
    }
```

## Hardware

* `GET /api/hardware`: Obtener todos los hardware
* `GET /api/hardware/{id}`: Obtener un hardware específico 
* `DELETE /api/hardware/{id}`: Eliminar un hardware específico
* `POST /api/hardware`: Crear un nuevo hardware, como por ejemplo :
 ```bash
   {
        "hardwareName": "Mouse"
    }
  ```
    
* `PUT /api/hardware/{id}`: Editar un hardware, como por ejemplo
```bash
   {
        "id": 1,
        "hardwareName": "Mouse Optico"
    }
```


## Software

* `GET /api/software`: Obtener todos los software 
* `GET /api/software/{id}`: Obtener un software específico 
* `DELETE /api/software/{id}`: Eliminar un software específico
* `POST /api/software`: Crear un nuevo software, como por ejemplo :
 ```bash
   {
        "softwareName": "e-Sword"
    }
  ```
    
* `PUT /api/software/{id}`: Editar un software, como por ejemplo :
```bash
   {
        "Id": 1,
        "softwareName": "e-Sword"
    }
```

## Asignaciones

* `GET /api/assignment`: Obtener todas los asignaciones
* `GET /api/assignment/user/{id}`: Asignaciones específicas por ID de usuario
* `POST /api/assignment`: Crear una nueva asignación, como por ejemplo :
 ```bash
   {
        "userID": 7,
        "softwareID": 7,
        "hardwareID": 7
    }
  ```

```
Nota: Api se ejecuta por defecto en el puerto 44362 con SSL, en Postman deshabilitado la verificación SSL