import { Route, Routes, Link, Navigate } from 'react-router-dom'
import Login from './pages/Login'
import Home from './pages/Home'
import InventorySearch from './pages/Inventory/InventorySearch'
import InventoryAdd from './pages/Inventory/InventoryAdd'
import InventoryEdit from './pages/Inventory/InventoryEdit'
import InventoryDetails from './pages/Inventory/InventoryDetails'
import EmployeeSearch from './pages/Employee/EmployeeSearch'
import EmployeeDetails from './pages/Employee/EmployeeDetails'
import EmployeeAdd from './pages/Employee/EmployeeAdd'
import EmployeeEdit from './pages/Employee/EmployeeEdit'
import SecuritySearch from './pages/Security/SecuritySearch'
import SecurityEdit from './pages/Security/SecurityEdit'
import SecurityAdd from './pages/Security/SecurityAdd'
import UnauthorizedPage from './pages/UnauthorizedPage'
import UnauthorizedAction from './pages/UnauthorizedAction'

function App() {

  return (
    <>
      <nav>
        <h1 className='text-2xl mb-[30px]'>INVENTORY MANAGEMENT SYSTEM</h1>
        <div className='bg-[#014880]'>
          <div className='py-[10px] flex max-w-[800px] text-white'>
            <Link className="px-[20px]" to="/inventory">Inventory Management</Link>
            <Link className="px-[20px]" to="/employee">Employee Management</Link>
            <Link className="px-[20px]" to="/security">Security Management</Link>
          </div>
        </div>
      </nav>

      <Routes>
        <Route index element={<Navigate to='/login'/>}/>
        <Route path='/login' element={<Login/>}/>
        <Route path='/home' element={<Home/>}/>
        <Route path='/unauthorizedpage' element={<UnauthorizedPage/>}/>
        <Route path='/unauthorizedaction' element={<UnauthorizedAction/>}/>
        <Route path='/inventory'>
          <Route index element={<InventorySearch/>}/>
          <Route path='add' element={<InventoryAdd/>}/>
          <Route path='edit/:id' element={<InventoryEdit/>}/>
          <Route path='details/:id' element={<InventoryDetails/>}/>
        </Route>
        <Route path='/employee'>
          <Route index element={<EmployeeSearch/>}/>
          <Route path='add' element={<EmployeeAdd/>}/>
          <Route path='edit/:id' element={<EmployeeEdit/>}/>
          <Route path='details/:id' element={<EmployeeDetails/>}/>
        </Route>
        <Route path='/security'>
          <Route index element={<SecuritySearch/>}/>
          <Route path='edit/:id' element={<SecurityEdit/>}/>
          <Route path='add' element={<SecurityAdd/>}/>
        </Route>
      </Routes>
    </>
  )
}

export default App
