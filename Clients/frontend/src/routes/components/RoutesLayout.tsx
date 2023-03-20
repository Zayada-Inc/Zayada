import { Outlet } from 'react-router-dom';

export const RoutesLayout = () => {
  return (
    <div className='App'>
      <Outlet />
    </div>
  );
};
