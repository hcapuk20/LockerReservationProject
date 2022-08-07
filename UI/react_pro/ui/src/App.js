import { Route, Routes } from 'react-router-dom';
import LoginPage from './page/LoginPage';
import AdminPage from './page/AdminPage';
import NonAdminPage from './page/NonAdminPage';
import NavigationPage from './page/NavigationPage';
import RequireAuth from './components/RequireAuth';
import EditPage from './page/EditPage';
import ContentsPage from './page/ContentsPage';
//
function App() {
  return (
    <Routes>
      <Route path="/" element={<LoginPage />} />
       <Route element={<RequireAuth />} >
        <Route path="/nonadmin" element={<NonAdminPage />} />
        <Route path="/admin" element={<AdminPage />} />
        <Route path="/navigationpage" element={<NavigationPage />} />
        <Route path="/editpage" element={<EditPage />} />
        <Route path="/contentspage" element={<ContentsPage />} />
      </Route>
      <Route path="*" element={<LoginPage />} />
    </Routes>
  );
}

export default App;
