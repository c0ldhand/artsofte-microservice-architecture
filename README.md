# 🏪 Учебный проект — Маркетплейс (Microservices Architecture)

В репозитории реализуется учебный проект **«Маркетплейс»**, основанный на **микросервисной архитектуре**.


## ⚙️ Микросервисы

| Микросервис | Описание |
|--------------|-----------|
| **Identity microservice** | Регистрация, авторизация, определение ролей (пользователь / админ) |
| **Payment microservice** | Оплата товара, возврат средств, хранение истории покупок |
| **Order microservice** | Управление заказами, создание заказов, хранение купленных товаров, статусы заказов |
| **Catalog microservice** | CRUD товаров, поиск, категории, добавление в избранное |
| **Cart microservice** | Корзина для покупки товаров |
| **Notifications microservice** | Отправка уведомлений пользователю |
| **Review microservice** | Добавление отзывов и оценок к товарам |

---

## 💡 Основная функциональность приложения

1. 👤 Регистрация и авторизация пользователей  
2. 💳 Возможность покупки товаров пользователем  
3. 🛠️ Модерация товаров администратором (CRUD)  
4. ⭐ Оставление отзывов и оценок к товарам  
5. 🔔 Система уведомлений  
6. 🚚 Статусы заказа:  
   - создан  
   - на складе  
   - в пути  
   - доставлен  
   - отменен  
7. 🛍️ Каталог товаров с поиском и категориями  

---

## 🧩 Сущности **Identity microservice**

### Таблица `Users`

| Поле | Тип в БД | Тип в C# |
|------|-----------|----------|
| Id | `uuid` | `Guid` |
| Name | `text` | `string` |
| Email | `text` | `string` |
| RoleId | `uuid` | `Guid` |
| PasswordHash | `text` | `string` |
| Created | `timestamp with time zone` | `DateTime` |
| Updated | `timestamp with time zone` | `DateTime` |

---

### Таблица `Roles`

| Поле | Тип в БД | Тип в C# |
|------|-----------|----------|
| Id | `uuid` | `Guid` |
| Name | `text` | `string` |

---

### Таблица `RefreshTokens`

| Поле | Тип в БД | Тип в C# |
|------|-----------|----------|
| Id | `uuid` | `Guid` |
| UserId | `uuid` | `Guid` |
| Token | `text` | `string` |
| ExpiresOn | `timestamp with time zone` | `DateTime` |
| IsRevoked | `bool` | `bool` |

---

## 🔗 API Endpoints (Identity Service)

| Метод | Путь | Описание |
|--------|------|-----------|
| **POST** | `/api/auth/register` | Регистрация пользователя |
| **POST** | `/api/auth/login` | Авторизация пользователя |
| **POST** | `/api/auth/admin/login` | Авторизация администратора |
| **POST** | `/api/auth/refresh` | Обновление Access Token по Refresh Token |
| **GET** | `/api/roles` | Получение всех ролей |
| **POST** | `/api/roles` | Создание новой роли |
| **GET** | `/api/roles/{id}` | Получение роли по ID |
| **PUT** | `/api/roles/{id}` | Изменение роли |
| **DELETE** | `/api/roles/{id}` | Удаление роли |
| **GET** | `/api/users` | Получение всех пользователей |
| **GET** | `/api/users/{id}` | Получение пользователя по ID |
| **PUT** | `/api/users/{id}` | Обновление пользователя |
| **DELETE** | `/api/users/{id}` | Удаление пользователя |

---

## 🛠️ Технологии

- **.NET 9 / ASP.NET Core**
- **Entity Framework Core (PostgreSQL)**
- **Scalar (OpenAPI)**
- **JWT Authentication**
- **Microservices Architecture**
- **REST API**

---
