import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Login } from "./components/pages/Login";
import { Register } from "./components/pages/Register";
import { UserInfo } from "./components/pages/UserInfo";
import { AdminUser } from "./components/pages/AdminUser";
import { AdminOrder } from "./components/pages/AdminOrder";
import { AdminProduct } from "./components/pages/AdminProduct";
import { UserHistory } from "./components/pages/UserHistory";
import { UserCart } from "./components/pages/UserCart";
import { UserProduct } from "./components/pages/UserProduct";


export default function App() {
  console.log("hello world");
  
  return (
    <BrowserRouter>
        <h1 className="h-screen flex w-full items-center bg-[#F6ECE7] justify-center">
          <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/login" element={<Login />} />
            <Route path="/admin/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/user/:id/info" element={<UserInfo />} />
            <Route path="/user/:id/cart" element={<UserCart />} />
            <Route path="/user/:id/products" element={<UserProduct />} />
            <Route path="/user/:id/orders" element={<UserHistory />} />
            <Route path="/admin/users" element={<AdminUser />} />
            <Route path="/admin/orders" element={<AdminOrder />} />
            <Route path="/admin/products" element={<AdminProduct />} />
          </Routes>
        </h1>
    </BrowserRouter>
  )
}