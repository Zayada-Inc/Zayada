import { Drawer } from 'components/Drawer';
import { Navbar } from 'components/Navbar';

interface ContentLayoutProps {
  children: React.ReactNode;
}

export const ContentLayout = ({ children }: ContentLayoutProps) => {
  return (
    <div className='grid grid-cols-[4%_1fr]'>
      <Drawer />
      <div className='flex flex-col h-screen'>
        <Navbar />
        <div className='basis-full'>{children}</div>
      </div>
    </div>
  );
};
