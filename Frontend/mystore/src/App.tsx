import { BrowserRouter, Route, Routes } from "react-router-dom";
import { AdminUser } from "./components/pages/AdminUser";
import { AdminOrder } from "./components/pages/AdminOrder";

export default function App() {
  console.log("hello world");
  
  return (
    <BrowserRouter>
        <h1 className="h-screen flex w-full items-center bg-[#F6ECE7] justify-center">
          <Routes>
            <Route path="/admin/users" element={<AdminUser />} />
            <Route path="/admin/orders" element={<AdminOrder />} />
          </Routes>
        </h1>
    </BrowserRouter>
  )
}