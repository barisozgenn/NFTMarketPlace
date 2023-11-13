import Heading from '@/app/components/Heading'
import React from 'react'
import AuctionForm from '../NFTAuctionForm'

export default function Create() {
  return (
    <div className='mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg'>
      <Heading title='Sell your nft!' subtitle='Please enter the details of your nft' />
      <AuctionForm />
    </div>
  )
}