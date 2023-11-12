import React from 'react'
import Search from './Search'
import Logo from './Logo'
//flex justify-between: Our nav bar divs are spread across the top of our browser window.
export default function Navbar() {
  return (
    <header className='sticky top-0 z-50 flex justify-between bg-gray-100 p-5 items-center text-gray-700 shadow-md'>
      <Logo />
      <Search />
      <div>Login</div>
    </header>
  )
}
