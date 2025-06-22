import './App.css';
import { CalcScore } from '../src/components/CalculateScore';

function App() {
  return (
    <div>
      <CalcScore Name={"Steeve"}
      School={"DNV Public School"}
      total={284}
      goal={3}
      />
    </div>
  );
}

export default App;
