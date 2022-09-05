import { Route, Routes } from 'react-router-dom';
import LoginPage from './page/LoginPage';
import AdminPage from './page/AdminPage';
import NavigationPage from './page/NavigationPage';
import RequireAuth from './components/RequireAuth';
import ContentsPage from './page/ContentsPage';
import DbManagerPage from './page/DbManagerPage';
import { CssBaseline } from '@mui/material/';


import { createTheme, ThemeProvider } from '@mui/material/styles'


const themeDark = createTheme({
  palette: {
    secondary: {
      main:'#dc1111',
    },
    primary: {
      main: '#4C79DE',
    },
        background: {
      default: '#D3D3F3',
       paper: '#FFFFFF',
    }
  },
});


function App() {
  return (

    <ThemeProvider theme={themeDark}>
      <CssBaseline enableColorScheme />
    <Routes>
      <Route path="/" element={<LoginPage />} />
       <Route element={<RequireAuth />} >
        <Route path="/admin" element={<AdminPage />} />
        <Route path="/navigationpage" element={<NavigationPage />} />
        <Route path="/contentspage" element={<ContentsPage />} />
        <Route path="/dbmanager" element={<DbManagerPage />} />
      </Route>
      <Route path="*" element={<LoginPage />} />
    </Routes>
    </ThemeProvider>
  );
}

export default App;
