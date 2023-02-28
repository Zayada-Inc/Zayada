import { useState } from 'react';
import reactLogo from './assets/react.svg';
import './App.css';

function App() {
  const [count, setCount] = useState(0);
  const config = {
    headers: {
      'Access-Control-Allow-Origin': '*',
    },
  };
  //test
  // axios.get('http://localhost:5000/api/PersonalTrainer/personalTrainers', config).then((res: any) => console.log(res.data))
  return (
    <div className='App'>
      <Test />
      <h1>Vite + React</h1>
      <div className='card'>
        <button onClick={() => setCount((count) => count + 1)}>count is {count}</button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className='read-the-docs'>Click on the Vite and React logos to learn more</p>
    </div>
  );
}

export default App;
