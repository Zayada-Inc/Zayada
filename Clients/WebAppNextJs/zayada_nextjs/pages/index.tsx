
import { Navbar, Button } from "flowbite-react";
import RootLayout from "./layout";

export default function Home() {
  return (
    <RootLayout>
    <main className=" min-h-screen w-screen bg-slate-200">
      <main className="min-h-screen w-screen bg-slate-200">
      <Navbar
  fluid={true}
  rounded={true}
  className="bg-white margin-0 border-b-2"
>
  <Navbar.Brand href="https://zayada.io/">
    <span className="self-center whitespace-nowrap text-xl font-semibold dark:text-white">
      Zayada
    </span>
  </Navbar.Brand>
  <div className="flex md:order-2">
    <Button className="mr-3">
      Get started
    </Button>
    <Navbar.Toggle className="mr-2"/>
  </div>
  <Navbar.Collapse>
    <Navbar.Link
      href="/navbars"
      active={true}
    >
      Home
    </Navbar.Link>
    <Navbar.Link href="/navbars">
      About
    </Navbar.Link>
    <Navbar.Link href="/navbars">
      Services
    </Navbar.Link>
    <Navbar.Link href="/navbars">
      Pricing
    </Navbar.Link>
    <Navbar.Link href="/navbars">
      Contact
    </Navbar.Link>
  </Navbar.Collapse>
</Navbar>
        <div className="container mx-auto">
          <div className="flex flex-col items-center justify-center min-h-screen py-2">
            <h1 className="text-4xl font-bold text-gray-800 dark:text-white text-center border-spacing-1 ">
              Hello DuDu! :)
            </h1>
          </div>
        </div>
      </main>
    </main>
    </RootLayout>
  );

}
