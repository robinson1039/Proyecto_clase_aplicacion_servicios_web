import { BrowserRouter, Routes, Route } from "react-router-dom";
import Pages from "./pages/RedPages/RedPages";
import CreateRed from "./pages/RedPages/CreateRedPages";
import EditRed from "./pages/RedPages/EditRedPages";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Pages />} />
        <Route path="/crear-red" element={<CreateRed />} />
        <Route path="/editar-red/:id" element={<EditRed />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;