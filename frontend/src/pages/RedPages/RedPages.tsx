import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import clienteAxios from "../../api/axios";
import Swal from "sweetalert2";

interface Red {
  id: number;
  nombre: string;
  url: string;
  pais: string;
}

const Redes: React.FC = () => {

  const [redes, setRedes] = useState<Red[]>([]);

const fetchRedes = async () => {
  try {

    const response = await clienteAxios.get("/RedApi");

    const redesData = response.data?.data || response.data;

    setRedes(redesData ?? []);

  } catch (error) {
    console.error("Error obteniendo redes:", error);
    setRedes([]);
  }
};

  const handleDelete = async (id: number) => {

    try {

      const result = await Swal.fire({
        title: "¿Eliminar red?",
        text: "Esta acción no se puede deshacer",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Eliminar",
      });

      if (result.isConfirmed) {

        const response = await clienteAxios.delete(
          `/RedApi/${id}`
        );

        Swal.fire({
          icon: "success",
          title: "Eliminado",
          text: response.data.message,
        });

        fetchRedes();
      }

    } catch (error: any) {

      console.error("Error eliminando red:", error);

      Swal.fire({
        icon: "error",
        title: "Error",
        text: error.response?.data?.message || "Error desconocido",
      });

    }
  };

  useEffect(() => {
    fetchRedes();
  }, []);

  return (
    <div className="p-6 bg-gray-100 min-h-screen">

      <h1 className="text-2xl font-bold mb-6 text-center">
        Lista de Redes
      </h1>

      <div className="flex justify-center mb-6 gap-4">

        <button
          onClick={fetchRedes}
          className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
        >
          Ver todas
        </button>

        <Link
          to="/crear-red"
          className="px-6 py-2 bg-green-600 text-white rounded hover:bg-green-700"
        >
          Crear Red
        </Link>

      </div>

      <div className="overflow-x-auto">

        <table className="w-full bg-white rounded-lg shadow">

          <thead className="bg-blue-100">
            <tr>
              <th className="px-4 py-2 text-center">ID</th>
              <th className="px-4 py-2 text-center">Nombre</th>
              <th className="px-4 py-2 text-center">URL</th>
              <th className="px-4 py-2 text-center">País</th>
              <th className="px-4 py-2 text-center">Acciones</th>
            </tr>
          </thead>

          <tbody>

            {redes.length > 0 ? (

              redes.map((red) => (

                <tr key={red.id}>

                  <td className="px-4 py-2 text-center">
                    {red.id}
                  </td>

                  <td className="px-4 py-2 text-center">
                    {red.nombre}
                  </td>

                  <td className="px-4 py-2 text-center">
                    {red.url}
                  </td>

                  <td className="px-4 py-2 text-center">
                    {red.pais}
                  </td>

                  <td className="px-4 py-2 text-center">

                    <div className="flex gap-2 justify-center">

                      <Link
                        to={`/editar-red/${red.id}`}
                        className="bg-yellow-500 text-white px-3 py-1 rounded hover:bg-yellow-600"
                      >
                        Editar
                      </Link>

                      <button
                        onClick={() => handleDelete(red.id)}
                        className="bg-red-500 text-white px-3 py-1 rounded hover:bg-red-600"
                      >
                        Eliminar
                      </button>

                    </div>

                  </td>

                </tr>

              ))

            ) : (

              <tr>

                <td
                  colSpan={5}
                  className="text-center px-4 py-4 text-red-500"
                >
                  No hay redes registradas
                </td>

              </tr>

            )}

          </tbody>

        </table>

      </div>

    </div>
  );
};

export default Redes;