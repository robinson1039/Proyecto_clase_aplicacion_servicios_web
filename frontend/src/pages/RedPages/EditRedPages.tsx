import React, { useEffect, useState, type FormEvent } from "react";
import { useParams, useNavigate } from "react-router-dom";
import clienteAxios from "../../api/axios";
import Swal from "sweetalert2";


interface Red {
  id: number;
  nombre: string;
  url: string;
  pais: string;
}

const EditRed: React.FC = () => {

  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [red, setRed] = useState<Red>({
    id: 0,
    nombre: "",
    url: "",
    pais: ""
  });

  useEffect(() => {

    const fetchRed = async () => {

      try {

        const response = await clienteAxios.get(`/RedApi/${id}`);

        setRed(response.data);

      } catch (error) {

        console.error("Error cargando red:", error);

        Swal.fire(
          "Error",
          "No se pudo cargar la red",
          "error"
        );

      }

    };

    fetchRed();

  }, [id]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {

    setRed({
      ...red,
      [e.target.name]: e.target.value
    });

  };

 const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
  e.preventDefault();

  try {
    const response = await clienteAxios.put("/RedApi", red);

    Swal.fire(
      "Actualizado",
      response.data.message,
      "success"
    );

    navigate("/");
  } catch (error: any) {
    console.error("Error actualizando:", error);

    Swal.fire(
        "Error",
        error.response?.data?.message || "No se pudo actualizar la red",
        "error"
        );
    }
    };

  return (

    <div className="p-6 bg-gray-100 min-h-screen">

      <h2 className="text-2xl font-bold mb-4 text-center">
        Editar Red
      </h2>

      <form
        onSubmit={handleSubmit}
        className="bg-white shadow rounded p-6 max-w-lg mx-auto"
      >

        <div className="mb-4">
          <label className="block font-semibold mb-1">
            Nombre
          </label>

          <input
            type="text"
            name="nombre"
            value={red.nombre}
            onChange={handleChange}
            className="w-full border border-gray-300 p-2 rounded"
          />
        </div>

        <div className="mb-4">
          <label className="block font-semibold mb-1">
            URL
          </label>

          <input
            type="text"
            name="url"
            value={red.url}
            onChange={handleChange}
            className="w-full border border-gray-300 p-2 rounded"
          />
        </div>

        <div className="mb-4">
          <label className="block font-semibold mb-1">
            País
          </label>

          <input
            type="text"
            name="pais"
            value={red.pais}
            onChange={handleChange}
            className="w-full border border-gray-300 p-2 rounded"
          />
        </div>

        <button
          type="submit"
          className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
        >
          Guardar Cambios
        </button>

      </form>

    </div>

  );

};

export default EditRed;