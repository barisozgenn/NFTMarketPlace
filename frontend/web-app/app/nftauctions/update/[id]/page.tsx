import Heading from '@/app/components/Heading'
import React from 'react'
import AuctionForm from '../../NFTAuctionForm'
import { getDetailedViewData } from '@/app/actions/nftAuctionAction'

export default async function Update({params}: {params: {id: string}}) {
  const data = await getDetailedViewData(params.id);

  return (
    <div className='mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg'>
      <Heading title='Update your nft auction' subtitle='Please update the details of your nft' />
      <AuctionForm nftAuction={data} />
    </div>
  )
}