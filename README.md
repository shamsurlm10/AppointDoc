# AppointDoc API Documentation

## 📖 Overview
The **AppointDoc API** allows users to register, log in, and manage medical appointments. It provides secure endpoints to create, retrieve, update, and delete appointments using **JWT-based authentication**.

---

## 🔐 Authentication

### 📝 Register a New User
- **Endpoint:** `POST /api/Auth/Register`
- **Description:** Registers a new user account.
- **Request Body:**
  ```json
  {
      "username": "string",
      "password": "string"
  }
  ```
- **Response:**
  - `200 OK`: Registration successful.

### 🔑 User Login
- **Endpoint:** `POST /api/Auth/Login`
- **Description:** Authenticates a user and returns a JWT token.
- **Request Body:**
  ```json
  {
      "username": "string",
      "password": "string"
  }
  ```
- **Response:**
  - `200 OK`: Returns a JWT token for authentication.
- **Authorization Header Format:**
  ```
  Authorization: Bearer <token>
  ```

---

## 📅 Appointment Endpoints

### ➕ Create an Appointment
- **Endpoint:** `POST /api/Appointment/CreateAppointment`
- **Description:** Creates a new appointment.
- **Headers:**
  - `Authorization: Bearer <token>`
  - `Content-Type: application/json`
- **Request Body:**
  ```json
  {
      "patientName": "string",
      "patientContactInformation": "string",
      "appointmentDateTime": "YYYY-MM-DDTHH:MM:SS",
      "doctorId": "GUID"
  }
  ```
- **Response:**
  - `200 OK`: Appointment created successfully.

### 📄 Get All Appointments
- **Endpoint:** `GET /api/Appointment/GetAllAppointments`
- **Description:** Retrieves all appointments.
- **Headers:**
  - `Authorization: Bearer <token>`
- **Response:**
  - `200 OK`: Returns a list of all appointments.

### 🔍 Get Appointment by ID
- **Endpoint:** `GET /api/Appointment/{id}`
- **Description:** Retrieves a specific appointment by its ID.
- **Path Parameter:**
  - `id`: GUID of the appointment
- **Headers:**
  - `Authorization: Bearer <token>`
- **Response:**
  - `200 OK`: Appointment details.

### ✏️ Update an Appointment
- **Endpoint:** `PUT /api/Appointment/{id}`
- **Description:** Updates an existing appointment.
- **Path Parameter:**
  - `id`: GUID of the appointment
- **Headers:**
  - `Authorization: Bearer <token>`
  - `Content-Type: application/json`
- **Request Body:**
  ```json
  {
      "patientName": "string",
      "patientContactInformation": "string",
      "appointmentDateTime": "YYYY-MM-DDTHH:MM:SS",
      "doctorId": "GUID"
  }
  ```
- **Response:**
  - `200 OK`: Appointment updated successfully.

### ❌ Delete an Appointment
- **Endpoint:** `DELETE /api/Appointment/{id}`
- **Description:** Deletes an appointment.
- **Path Parameter:**
  - `id`: GUID of the appointment
- **Headers:**
  - `Authorization: Bearer <token>`
- **Response:**
  - `200 OK`: Appointment deleted successfully.

---

## 📦 Data Models

### 📌 AppointmentRequestDto
```json
{
    "patientName": "string",
    "patientContactInformation": "string",
    "appointmentDateTime": "YYYY-MM-DDTHH:MM:SS",
    "doctorId": "GUID"
}
```

### 📌 LoginRegisterRequest
```json
{
    "username": "string",
    "password": "string"
}
```

---

## 🔒 Security

- **Authentication Method:** JWT (Bearer Token)
- **Header Format:**
  ```
  Authorization: Bearer <token>
  ```
- **Security Scheme:**
  ```json
  {
      "type": "apiKey",
      "description": "Please enter 'Bearer' [space] and then your token",
      "name": "Authorization",
      "in": "header"
  }
  ```

---

## 🛠️ Testing the API

### 📬 Using Postman
1. **Register** via `POST /api/Auth/Register`.
2. **Login** via `POST /api/Auth/Login` and copy the token.
3. **Authorize** by adding the token to the Authorization header as `Bearer <token>`.
4. **Test Endpoints** (Create, Get, Update, Delete appointments).

### 🌐 Using Swagger UI
1. **Run** the application.
2. Go to `http://localhost:<port>/swagger`.
3. Use the **Authorize** button to enter the Bearer token.
4. **Test** each API endpoint directly.

---

## 🚦 Response Codes

- `200 OK`: Operation successful.
- `401 Unauthorized`: Invalid or missing token.
- `404 Not Found`: Resource not found.
- `500 Internal Server Error`: Server-side error.

---

