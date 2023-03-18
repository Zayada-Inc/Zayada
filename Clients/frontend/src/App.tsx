import { Navigate, Route, Routes } from 'react-router-dom';
import 'lib/translation/i18nConfig';

import { Dashboard } from 'features/Dashboard';
import { Register } from 'features/auth/components';

function App() {
  return (
    <div className='App'>
      {/* TO-DO: create proper routing */}
      <Routes>
        <Route path='/' element={<Navigate to='/register' />} />
        <Route path='/dashboard' element={<Dashboard />} />
        <Route path='/trainers' />
        <Route path='/register' element={<Register />} />
      </Routes>
    </div>
  );
}

export default App;
