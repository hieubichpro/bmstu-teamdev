import { BrowserRouter, Route, Routes } from "react-router-dom";
import { AdminUser } from "./components/pages/AdminUser";
import { AdminOrder } from "./components/pages/AdminOrder";
import { AdminProduct } from "./components/pages/AdminProduct";

export default function App() {
  console.log("hello world");
  
  return (
    <BrowserRouter>
        <h1 className="h-screen flex w-full items-center bg-[#F6ECE7] justify-center">
          <Routes>
            <Route path="/admin/users" element={<AdminUser />} />
            <Route path="/admin/orders" element={<AdminOrder />} />
            <Route path="/admin/products" element={<AdminProduct />} />
          </Routes>
        </h1>
    </BrowserRouter>
  )
}