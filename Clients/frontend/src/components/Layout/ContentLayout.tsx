import { Navbar } from 'components/Navbar';

interface ContentLayoutProps {
  children: React.ReactNode;
}

export const ContentLayout = ({ children }: ContentLayoutProps) => {
  return (
    <>
      <Navbar />
      <div>{children}</div>
    </>
  );
};
