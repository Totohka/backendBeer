import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './App.css';
import {SignInPage, RegisterPage, ProfilePage, SettingsPage, SignInFirstPage, AdminPage } from './app/routes';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='' element={<SignInPage />}/>
        <Route path='/signin' element={<SignInPage />}/>
        <Route path='/signinfirst' element={<SignInFirstPage />}/>
        <Route path='/register' element={<RegisterPage />}/>
        <Route path='/profile' element={<ProfilePage />} />
        <Route path='/settings' element={<SettingsPage />} />
        <Route path='/admin' element={<AdminPage />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
