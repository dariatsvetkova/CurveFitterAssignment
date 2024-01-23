import './App.css';
import { CurrentPlot, Layout, UserInputs, UserPlots } from './components';

export default function App() {
    return (
        <Layout>
            <UserInputs />
            <CurrentPlot />
            <UserPlots />
        </Layout>
    )
}
