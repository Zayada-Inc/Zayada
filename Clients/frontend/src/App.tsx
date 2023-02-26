import { useState } from 'react'
import reactLogo from './assets/react.svg'
import './App.css'
import axios from 'axios'

function App() {
  const [count, setCount] = useState(0)
<<<<<<< HEAD
<<<<<<< Updated upstream

=======
=======
>>>>>>> 9b29f968a4e757918a6c9e3a397e78d3eb8985ca
  const config = {
    headers: {
      "Access-Control-Allow-Origin": '*',
    }
  }
<<<<<<< HEAD
  //test
  // axios.get('http://localhost:5000/api/PersonalTrainer/personalTrainers', config).then((res: any) => console.log(res.data))
>>>>>>> Stashed changes
=======
  axios.get('http://localhost:5000/api/PersonalTrainer/personalTrainers', config).then((res: any) => console.log(res.data))
>>>>>>> 9b29f968a4e757918a6c9e3a397e78d3eb8985ca
  return (
    <div className="App">
      <div>
        <a href="https://vitejs.dev" target="_blank">
          <img src="/vite.svg" className="logo" alt="Vite logo" />
        </a>
        <a href="https://reactjs.org" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </div>
  )
}

export default App
