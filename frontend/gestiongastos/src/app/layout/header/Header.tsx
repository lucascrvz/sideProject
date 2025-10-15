import Link from "next/link";
import React from "react";

const navItems = [
  { href: "/", label: "HOME" },
  { href: "/ingreso-gastos", label: "INGRESA GASTOS" },
  { href: "/dashboard", label: "DASHBOARD" },
  { href: "/perfil", label: "PERFIL" },
];

export default function Header() {
  return (
    <header className="shadow-lg">
      <nav className="container mx-auto px-4 py-4">
        <ul className="flex space-x-6">
          {navItems.map((item) => (
            <li key={item.href}>
              <Link
                href={item.href}
                className=" transition-colors duration-200 font-medium"
              >
                {item.label}
              </Link>
            </li>
          ))}
        </ul>
      </nav>
    </header>
  );
}
