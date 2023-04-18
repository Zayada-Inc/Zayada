
import { Navbar, Button } from "flowbite-react";
import RootLayout from "./layout";

interface HomeProps {
  homeString: string;
}

export async function getStaticProps() {
  try {
  const res = await fetch("http://localhost:5001/");
  const data = await res.text();
  return {props: {homeString: data}};
  } catch (error) {
    return {props: {homeString: "NaN"}};
  }
}

const Home: React.FC<HomeProps> = ({homeString}) => {
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
              {homeString} :)
            </h1>
          </div>
        </div>
      </main>
    </main>
    <footer className="bg-white rounded-lg shadow dark:bg-gray-900 m-4">
    <div className="w-full max-w-screen-xl mx-auto p-4 md:py-8">
        <div className="sm:flex sm:items-center sm:justify-between">
            <a href="#" className="flex items-center mb-4 sm:mb-0">
                <img src="https://res.cloudinary.com/dgwkfkpve/image/upload/v1680804744/ozsrf72ko2ia9obn40iz.png" className="h-72 w-72" alt="Flowbite Logo" />
            </a>
            <ul className="flex flex-wrap items-center mb-6 text-sm font-medium text-gray-500 sm:mb-0 dark:text-gray-400">
                <li>
                    <a href="#" className="mr-4 hover:underline md:mr-6 ">About</a>
                </li>
                <li>
                    <a href="#" className="mr-4 hover:underline md:mr-6">Privacy Policy</a>
                </li>
                <li>
                    <a href="#" className="mr-4 hover:underline md:mr-6 ">Licensing</a>
                </li>
                <li>
                    <a href="#" className="hover:underline">Contact</a>
                </li>
            </ul>
        </div>
        <hr className="my-6 border-gray-200 sm:mx-auto dark:border-gray-700 lg:my-8" />
        <span className="block text-sm text-gray-500 sm:text-center dark:text-gray-400">© 2023 <a href="https://ZAYADA.com/" className="hover:underline">ZAYADA™</a>. All Rights Reserved.</span>
    </div>
</footer>
    </RootLayout>
  )
};

export default Home;