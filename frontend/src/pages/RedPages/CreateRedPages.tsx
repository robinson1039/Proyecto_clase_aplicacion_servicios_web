import React, { useState } from "react";
import clienteAxios from "../../api/axios";
import { useNavigate, Link } from "react-router-dom";
import Swal from "sweetalert2";

interface Red {
  nombre: string;
  url: string;
  pais: string;
}

const CreateRed: React.FC = () => {

  const navigate = useNavigate();

  const [red, setRed] = useState<Red>({
    nombre: "",
    url: "",
    pais: ""
  });

  const updateState = (e: React.ChangeEvent<HTMLInputElement>) => {
    setRed({
      ...red,
      [e.target.name]: e.target.value
    });
  };

  const addRed = async (e: React.FormEvent) => {
    e.preventDefault();

    try {

      const response = await clienteAxios.post("/RedApi", red);

      Swal.fire({
        icon: "success",
        title: "Red creada",
        text: response.data.message
      });

      navigate("/");

    } catch (error: any) {

      console.error("Error creando red:", error);

      Swal.fire({
        icon: "error",
        title: "Error",
        text: error.response?.data?.message || "Error desconocido"
      });

    }
  };

  return (

    <div className="flex justify-center items-center min-h-screen bg-gray-100 p-4">

      <form
        onSubmit={addRed}
        className="bg-white p-6 rounded-2xl shadow-md w-full max-w-lg space-y-4"
      >

        <h2 className="text-xl font-bold text-center">
          Crear Red
        </h2>

        <div>
          <label className="block text-sm font-medium mb-1">
            Nombre
          </label>

          <input
            type="text"
            name="nombre"
            value={red.nombre}
            onChange={updateState}
            className="w-full border border-gray-300 p-2 rounded"
            required
          />

        </div>

        <div>
          <label className="block text-sm font-medium mb-1">
            URL
          </label>

          <input
            type="text"
            name="url"
            value={red.url}
            onChange={updateState}
            className="w-full border border-gray-300 p-2 rounded"
          />

        </div>

        <div>
          <label className="block text-sm font-medium mb-1">
            País
          </label>

          <input
            type="text"
            name="pais"
            value={red.pais}
            onChange={updateState}
            className="w-full border border-gray-300 p-2 rounded"
          />

        </div>

        <div className="flex justify-between pt-4">

          <Link
            to="/"
            className="px-6 py-2 bg-gray-500 text-white rounded hover:bg-gray-600"
          >
            Volver
          </Link>

          <button
            type="submit"
            className="px-6 py-2 bg-green-600 text-white rounded hover:bg-green-700"
          >
            Guardar
          </button>

        </div>

      </form>

    </div>

  );
};

export default CreateRed;