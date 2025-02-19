# Monorepo Project: TCP Server and TCP Client(Web API + React Frontend)

This monorepo contains three interconnected applications:

1. **TcpServer (C#)** - A TCP server that generates and returns a random temperature (16°C - 46°C).
2. **TcpWebApi / TCP Client (C# Web API)** - A bridge between the React frontend and the TCP server.
3. **ReactFrontend (React + Tailwind CSS)** - A UI that displays the temperature received from the Web API.

## Project Structure
```
monorepo-project/
├── TcpServer/      # C# TCP Server
├── TcpWebApi/      # C# Web API
└── ReactFrontend/  # React Frontend (Axios + Tailwind CSS)
```

## Prerequisites
Ensure you have the following installed:
- **.NET SDK** (for C# applications)
- **Node.js & npm** (for React frontend)

---

## Setup & Running the Applications

### 1. Start the TCP Server
```sh
cd TcpServer
dotnet run
```
- The TCP server listens on **port 5000**.
- It returns a random temperature value between **16°C and 46°C**.

### 2. Start the Web API
```sh
cd TcpWebApi
dotnet run
```
- The Web API listens on **port 5282**.
- It fetches temperature data from the TCP server and exposes it via an endpoint.

### 3. Start the React Frontend
```sh
cd ReactFrontend
npm install  # Install dependencies
npm start    # Start the development server
```
- The React app connects to the Web API to fetch and display temperature data.

---

## How It Works
1. The **React Frontend** makes a request to the **Web API**.
2. The **Web API** forwards the request to the **TCP Server**.
3. The **TCP Server** responds with a random temperature.
4. The **Web API** sends this response back to the **React app**, which updates the UI.

### Example API Call
```sh
GET http://localhost:5001/temperature
```
**Response:**
```json
{
"message": "Temperature: 24°C"
}
```

---

## Technologies Used
- **Backend:** C# (.NET 7)
- **Frontend:** React + Tailwind CSS
- **Networking:** TCP Sockets
- **HTTP Requests:** Axios

---

## Future Enhancements
- Add authentication to the Web API.
- Implement a more detailed UI with historical temperature logs.
- Deploy all services using Docker.

---

## Contributing
Feel free to fork this repo and submit pull requests for improvements!

