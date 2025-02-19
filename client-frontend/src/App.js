// import logo from './logo.svg';
// import './App.css';
import { useEffect, useState } from "react";
import { getTemperature, getStatus } from "./services/api";
import GaugeComponent from "react-gauge-component";

function App() {
  const [temperature, setTemperature] = useState(0);
  const [status, setStatus] = useState(null);

  useEffect(() => {
    fetchTemperature();
  }, []);

  const fetchTemperature = async () => {
    try {
      const data = await getTemperature();
      const tempValue = parseFloat(data.message.replace(/[^\d.-]/g, ""));
      setTemperature(tempValue);
    } catch (error) {
      setTemperature(0);
    }
  };

  const fetchStatus = async () => {
    try {
      const data = await getStatus();
      setStatus(data.message);
    } catch (error) {
      setStatus("Error fetching status");
    }
  };

  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-sky-700 p-4">
      <h1 className="text-4xl font-bold mb-6 text-white">TCP Sensor Data</h1>

      {/* Large Gauge - Fully Centered */}
      <div className="flex justify-center items-center w-full h-[600px]  overflow-hidden">
  <GaugeComponent
    value={temperature}
    maxValue={100}
    arc={{
      colorArray: ["#00ff00", "#ffff00", "#ff0000" ],
      padding: 0,
      margin:0.3,
      width: 0.3,
    }}
    labels={{
      valueLabel: { formatTextValue: (value) => `${value}°C` },
    }}
    pointer={{
      type: "arrow",
      color: "#ff0000", 
      length: 0.8, 
      width: 5,
      elastic: true,
      animationDelay: 1500,
    }}
    size={500}
    style={{ width: "60%" }}
  />
</div>

      <div className="flex gap-6 mt-8">
        <button
          onClick={fetchTemperature}
          className="bg-blue-600 hover:bg-blue-700 text-white px-8 py-4 rounded-lg text-xl shadow-lg transition"
        >
          Get Temperature
        </button>

        <button
          onClick={fetchStatus}
          className="bg-green-600 hover:bg-green-700 text-white px-8 py-4 rounded-lg text-xl shadow-lg transition"
        >
          Get Status
        </button>
      </div>

      <p className="text-2xl mt-6 font-semibold text-white">Sensor is: {status?'Active':'Inactive'}</p>
      {/* Social Links */}
<footer className="mt-12">
  <div className="flex gap-8">
    <a
      href="https://github.com/MikiyasWT/tcp-communication-driver"
      target="_blank"
      rel="noopener noreferrer"
      className="text-xl font-semibold text-gray-800 hover:text-gray-600"
    >
      GitHub Source Code
    </a>
    <a
      href="https://www.linkedin.com/in/mikiyas-wendmneh/"
      target="_blank"
      rel="noopener noreferrer"
      className="text-xl font-semibold text-blue-700 hover:text-blue-500"
    >
      Lets Connect LinkedIn
    </a>
  </div>
</footer>
    </div>
  );
}

export default App;



