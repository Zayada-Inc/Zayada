import { useSelector } from 'react-redux';
import { Navigate, Outlet, useLocation } from 'react-router-dom';
import { selectUser } from 'store/slices/user';

export const ProtectedRoute = () => {
  const { username } = useSelector(selectUser);
  const location = useLocation();

  /* TO-DO: i18n this message */
  return username ? (
    <Outlet />
  ) : (
    <Navigate
      to='/login'
      state={{ from: location, message: 'You must be logged in to access that page' }}
      replace
    />
  );
};
