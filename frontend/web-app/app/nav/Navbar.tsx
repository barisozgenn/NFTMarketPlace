import React from 'react'
import {RiNftFill} from 'react-icons/Ri';// Ri: first 2 character where it is uploaded from
//flex justify-between: Our nav bar divs are spread across the top of our browser window.
export default function Navbar() {
  return (
    <header className='sticky top-0 z-50 flex justify-between bg-gray-100 p-5 items-center text-gray-700 shadow-md'>
      <div className='cursor-pointer flex items-center gap-2 text-3xl font-semibold text-purple-700'>
        <RiNftFill size={34}/>
        <div>NFT Market Place</div>
      </div>
      <div>middle</div>
      <div>right</div>
    </header>
  )
}
