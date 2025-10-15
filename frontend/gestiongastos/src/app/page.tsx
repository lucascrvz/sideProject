import { Children } from "react";
import { Header, Footer, SideNav } from "@/app/layout/index";

export default function Home({ children }: { children: React.ReactNode }) {
  return (
    <div>
      <Header />
      {Children.map(children, (child) => child)}
      <Footer />
    </div>
  );
}
