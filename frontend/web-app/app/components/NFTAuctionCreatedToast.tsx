import { NFTAuction } from '@/types'
import Image from 'next/image'
import Link from 'next/link'
import React from 'react'

type Props = {
    nftAuction: NFTAuction
}

export default function NFTAuctionCreatedToast({nftAuction}: Props) {
  return (
    <Link href={`/nftauctions/details/${nftAuction.id}`} className='flex flex-col items-center'>
        <div className='flex flex-row items-center gap-2'>
            <Image
                src={nftAuction.contentUrl}
                alt='image'
                height={80}
                width={80}
                className='rounded-lg w-auto h-auto'
            />
            <span>New Auction! {nftAuction.name} {nftAuction.collection} has been added</span>
        </div>
    </Link>
  )
}