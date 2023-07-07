import { Route, Routes } from 'react-router-dom';
import 'lib/translation/i18nConfig';

import { Dashboard } from 'features/Dashboard';
import { Login, Register } from 'features/auth';
import { Missing } from 'features/misc';
import { ProtectedRoute, RoutesLayout } from 'routes';
import { Scheduler } from 'features/Scheduler';

function App() {
  return (
    <Routes>
      <Route path='/' element={<RoutesLayout />}>
        {/* public */}
        <Route path='register' element={<Register />} />
        <Route path='login' element={<Login />} />
        <Route path='unauthorized' element={<Login />} />
        {/* protected */}
        <Route element={<ProtectedRoute />}>
          <Route path='dashboard' element={<Dashboard />} />
          <Route path='scheduler' element={<Scheduler />} />
        </Route>
        {/* missing */}
        <Route path='*' element={<Missing />} />
      </Route>
    </Routes>
  );
}

export default App;
