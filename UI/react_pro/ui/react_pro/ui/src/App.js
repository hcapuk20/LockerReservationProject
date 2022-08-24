import { Route, Routes } from 'react-router-dom';
import LoginPage from './page/LoginPage';
import AdminPage from './page/AdminPage';
import NonAdminPage from './page/NonAdminPage';
import NavigationPage from './page/NavigationPage';
import RequireAuth from './components/RequireAuth';
import ContentsPage from './page/ContentsPage';
import DbManagerPage from './page/DbManagerPage';
//
function App() {
  return (
    <Routes>
      <Route path="/" element={<LoginPage />} />
       <Route element={<RequireAuth />} >
        <Route path="/nonadmin" element={<NonAdminPage />} />
        <Route path="/admin" element={<AdminPage />} />
        <Route path="/navigationpage" element={<NavigationPage />} />
        <Route path="/contentspage" element={<ContentsPage />} />
        <Route path="/dbmanager" element={<DbManagerPage />} />
      </Route>
      <Route path="*" element={<LoginPage />} />
    </Routes>
  );
}

export default App;
