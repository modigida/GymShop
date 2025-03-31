# GymShop API - API Specifikation

## Översikt

* **Version:** 1.0
* **Bas-URL:** /api/
* **Format:** JSON

##

## Autentisering

### **Login**

**Endpoint:**  

> ```http
> POST /api/auth/login
> ```

**Beskrivning:** Logga in en användare.

#### **Request Body**  

```json
{
  "email": "user@example.com",
  "password": "securepassword"
}
```

#### **Response**

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Inloggning lyckades   |
| `401 Unauthorized` | Felaktiva uppgifter      |


## Kategorier

### **Hämta kategorier**

**Endpoint:**  

> ```http
> GET /api/Categories
> ```

**Beskrivning:** Hämtar en lista över alla kategorier.

#### **Response**

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  En lista med alla kategorier returneras   |
| `404 Undocumented` | Inga kategorier hittade      |


### **Hämta en kategori**

**Endpoint:**  

> ```http
> GET /api/Categories{id}
> ```

**Beskrivning:** Hämtar en kategori baserat på id.

#### **Response**

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  En kategori returneras   |
| `404 Undocumented` | Ingen kategori hittad      |


## Ordrar

**Endpoint:**  

> ```http
> GET /api/Orders
> ```

**Beskrivning:** Hämtar alla ordrar.

#### **Response**

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  En lista med alla ordrar returneras   |

**Endpoint:**  

> ```http
> GET /api/Orders{id}
> ```

**Beskrivning:** Hämtar en order baserat på id.

#### **Response**

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  En order returneras   |

**Endpoint:**  

> ```http
> POST /api/Orders
> ```

**Beskrivning:** Skapa en ny order.

```json
{
  "user": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "firstName": "string",
    "lastName": "string",
    "email": "string",
    "phone": "string",
    "address": "string",
    "role": {
      "id": 0,
      "name": "string"
    }
  },
  "purchaseDate": "2025-03-28T08:58:53.691Z",
  "orderStatus": {
    "id": 0,
    "name": "string"
  },
  "orderProducts": [
    {
      "quantity": 0,
      "currentPrice": 0,
      "productName": "string",
      "productId": 0
    }
  ]
}
```

#### **Response**

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Order skapad   |

**Endpoint:**  

> ```http
> PUT /api/Orders{id}
> ```

**Beskrivning:** Uppdatera en existerande order.

#### **Request Body**  

```json
{
  "id": 0,
  "user": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "firstName": "string",
    "lastName": "string",
    "email": "string",
    "phone": "string",
    "address": "string",
    "role": {
      "id": 0,
      "name": "string"
    }
  },
  "purchaseDate": "2025-03-28T08:58:53.691Z",
  "orderStatus": {
    "id": 0,
    "name": "string"
  },
  "orderProducts": [
    {
      "quantity": 0,
      "currentPrice": 0,
      "productName": "string",
      "productId": 0
    }
  ]
}
```

#### **Response**

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Uppdaterad order returneras   |
| `404 Undocumented` | Ingen order hittad      |

**Endpoint:**  

> ```http
> DELETE /api/Orders{id}
> ```

**Beskrivning:** Radera en order.

#### **Response**

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Ordern raderad   |
| `404 Undocumented` | Ingen order hittad      |


## Order produkter

### Hämta en order produkt

**Endpoint:**  

> ```http
> GET /api/OrderProducts/{orderId}/{productId}
> ```

**Beskrivning:** Hämta en order produkt baserat på orderId och produktId.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Order produkten returneras   |
| `404 Undocumented` | Ingen order produkt hittad      |


## Orderstatusar

### Hämta alla orderstatusar

**Endpoint:**  

> ```http
> GET /api/OrderStatuses
> ```

**Beskrivning:** Hämta alla orderstatusar.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  En lista med orderstatusar returneras   |
| `404 Undocumented` | Inga orderstatusar hittade      |

### Hämta en orderstatus

**Endpoint:**  

> ```http
> GET /api/OrderStatuses/{id}
> ```

**Beskrivning:** Hämta en orderstatus.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Orderstatus returneras   |
| `404 Undocumented` | Ingen orderstatus hittad      |

## Produkter

### Hämta alla produkter

**Endpoint:**  

> ```http
> GET /api/Products
> ```

**Beskrivning:** Hämta alla produkter.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  En lista med alla produkter returneras   |
| `404 Undocumented` | Inga produkter hittade      |

### Hämta en produkt

**Endpoint:**  

> ```http
> GET /api/Products/{id}
> ```

**Beskrivning:** Hämta en produkt.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Produkten returneras   |
| `404 Undocumented` | Ingen produkt hittad      |

### Skapa ny produkt

**Endpoint:**  

> ```http
> POST /api/Products
> ```

**Beskrivning:** Skapa en ny produkt.

#### **Request Body**  

```json
{
  "name": "string",
  "category": {
    "id": 0,
    "name": "string"
  },
  "productStatus": {
    "id": 0,
    "name": "string"
  },
  "balance": 0,
  "price": 0,
  "description": "string",
  "imageUrl": "string"
}
```

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Produkten skapad   |

### Uppdatera produkt

**Endpoint:**  

> ```http
> PUT /api/Products/{id}
> ```

**Beskrivning:** Uppdatera en existerande produkt.

#### **Request Body**  

```json
{
  "id": 0,
  "name": "string",
  "category": {
    "id": 0,
    "name": "string"
  },
  "productStatus": {
    "id": 0,
    "name": "string"
  },
  "balance": 0,
  "price": 0,
  "description": "string",
  "imageUrl": "string"
}
```

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Uppdaterad produkt returneras   |
| `404 Undocumented` | Ingen produkt hittad      |

### Ta bort produkt

**Endpoint:**  

> ```http
> DELETE /api/Products/{id}
> ```

**Beskrivning:** Radera en produkt.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Produkten raderad   |
| `404 Undocumented` | Ingen produkt hittad      |

## Produktstatusar

### Hämta alla produktstatusar

**Endpoint:**  

> ```http
> GET /api/ProductStatuses
> ```

**Beskrivning:** Hämta alla produktstatusar.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  En lista med alla produktstatusar returneras   |
| `404 Undocumented` | Inga produktstatusar hittade      |

### Hämta en produktstatus

**Endpoint:**  

> ```http
> GET /api/ProductStatuses/{id}
> ```

**Beskrivning:** Hämta en produktstatus.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Produktstatus returneras   |
| `404 Undocumented` | Ingen produktstatus hittad      |

## Roller

### Hämta alla roller

**Endpoint:**  

> ```http
> GET /api/Roles
> ```

**Beskrivning:** Hämta alla roller.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  En lista med alla roller returneras   |
| `404 Undocumented` | Inga roller hittade      |

### Hämta en roll

**Endpoint:**  

> ```http
> GET /api/Roles/{id}
> ```

**Beskrivning:** Hämta en produkt.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Rollen returneras   |
| `404 Undocumented` | Ingen roll hittad      |

## Användare

### Hämta alla användare

**Endpoint:**  

> ```http
> GET /api/Users
> ```

**Beskrivning:** Hämta alla användare.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  En lista med alla användare returneras   |
| `404 Undocumented` | Inga användare hittade      |

### Hämta alla kunder

**Endpoint:**  

> ```http
> GET /api/Users/customers
> ```

**Beskrivning:** Hämta alla kunder.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  En lista med alla kunder returneras   |
| `404 Undocumented` | Inga kunder hittade      |

### Hämta en användare

**Endpoint:**  

> ```http
> GET /api/Users/{id}
> ```

**Beskrivning:** Hämta en användare.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  En användare returneras   |
| `404 Undocumented` | Ingen användare hittades      |

### Registrera användare

**Endpoint:**  

> ```http
> POST /api/Users/register
> ```

**Beskrivning:** Registrera en ny användare.

#### **Request Body**  

```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phone": "string",
  "address": "string",
  "password": "string",
  "roleId": 0
}
```

#### **Return Body**  

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phone": "string",
  "address": "string",
  "role": {
    "id": 0,
    "name": "string"
  }
}
```

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Registrerad användare returneras   |

### Validera användaruppgifter

**Endpoint:**  

> ```http
> POST /api/Users/validate
> ```

**Beskrivning:** Validera användares lösenord.

#### **Request Body**  

```json
{
  "email": "string",
  "password": "string"
}
```

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Användare validerad   |
| `400 Bad Request`    |  Felaktiga användaruppgifter   |

### Logga in användare

**Endpoint:**  

> ```http
> POST /api/Users/login
> ```

**Beskrivning:** Logga in användare

#### **Request Body**  

```json
{
  "email": "string",
  "password": "string"
}
```

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Användare inloggad, token returneras   |
| `400 Bad Request`    |  Felaktiga användaruppgifter   |

### Uppdatera användare

**Endpoint:**  

> ```http
> PUT /api/Users/{id}
> ```

**Beskrivning:** Uppdatera en existerande användare.

#### **Request Body**  

```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phone": "string",
  "address": "string",
  "password": "string",
  "roleId": 0
}
```

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Uppdaterad användare returneras   |
| `404 Undocumented` | Ingen användare hittades      |

### Radera användare

**Endpoint:**  

> ```http
> DELETE /api/Users/{id}
> ```

**Beskrivning:** Radera användare.

| Statuskod      |  Beskrivning                |
|------------|----------------------------|
| `200 OK`    |  Användare raderad   |
| `404 Undocumented` | Ingen användare hittades      |