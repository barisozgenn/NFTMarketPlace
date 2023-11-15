'use client'

import { useNFTAuctionStore } from '@/hooks/useNFTAuctionStore';
import { useBidStore } from '@/hooks/useBidStore';
import { NFTAuction, NFTAuctionFinished, Bid } from '@/types';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr'
import { User } from 'next-auth';
import React, { ReactNode, useEffect, useState } from 'react'
import { toast } from 'react-hot-toast';
import NFTAuctionCreatedToast from '../components/NFTAuctionCreatedToast';
import { getDetailedViewData } from '../actions/nftAuctionAction';
import NFTAuctionFinishedToast from '../components/NFTAuctionFinishedToast';

type Props = {
    children: ReactNode
    user: User | null
}

export default function SignalRProvider({ children, user }: Props) {
    const [connection, setConnection] = useState<HubConnection | null>(null);
    const setCurrentPrice = useNFTAuctionStore(state => state.setCurrentPrice);
    const addBid = useBidStore(state => state.addBid);
    const apiUrl = process.env.NODE_ENV === 'production'
        ? 'https://api.nftmarketplace.com/notifications'
        : process.env.NEXT_PUBLIC_NOTIFY_URL

        useEffect(() => {
            const newConnection = new HubConnectionBuilder()
                .withUrl('http://localhost:6001/notifications')
                .withAutomaticReconnect()
                .build();

            setConnection(newConnection);
        }, []);

   /* useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl(apiUrl!)
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, [apiUrl]);*/

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(() => {
                    console.log('Connected to notification hub');

                    connection.on('BidPlaced', (bid: Bid) => {
                        if (bid.bidStatus.includes('Accepted')) {
                            setCurrentPrice(bid.nftAuctionId, bid.price);
                        }
                        addBid(bid);
                    });

                    connection.on('NFTAuctionCreated', (auction: NFTAuction) => {
                        if (user?.username !== auction.seller) {
                            return toast(<NFTAuctionCreatedToast nftAuction={auction} />, 
                                {duration: 10000})
                        }
                    });

                    connection.on('NFTAuctionFinished', (finishedAuction: NFTAuctionFinished) => {
                        const auction = getDetailedViewData(finishedAuction.nftAuctionId);
                        return toast.promise(auction, {
                            loading: 'Loading',
                            success: (auction) => 
                                <NFTAuctionFinishedToast 
                                    finishedAuction={finishedAuction} 
                                    nftAuction={auction}
                                />,
                            error: (err) => 'NFT Auction finished!'
                        }, {success: {duration: 10000, icon: null}})
                    })


                }).catch(error => console.log(error));
        }

        return () => {
            connection?.stop();
        }
    }, [connection, setCurrentPrice, addBid, user?.username])

    return (
        children
    )
}