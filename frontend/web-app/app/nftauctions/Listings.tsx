'use client'

import React, { useEffect, useState } from 'react'
import { NFTAuction, PagedResult } from '@/types';
import NFTAuctionCard from './NFTAuctionCard';
import AppPagination from '../components/AppPagination';
import { getData } from '../actions/nftAuctionAction';
import Filters from './Filters';
import { useParamsStore } from '@/hooks/useParamsStore';
import { shallow } from 'zustand/shallow';
import qs from 'query-string';
import EmptyFilter from '../components/EmptyFilter';
import { useNFTAuctionStore } from '@/hooks/useNFTAuctionStore';

export default function Listings() {
    const [loading, setLoading] = useState(true);

    const params = useParamsStore(state => ({
        pageNumber: state.pageNumber,
        pageSize: state.pageSize,
        searchTerm: state.searchTerm,
        orderBy: state.orderBy,
        filterBy: state.filterBy,
        seller: state.seller,
        winner: state.winner
    }), shallow)

    const data = useNFTAuctionStore(state => ({
        nftAuctions: state.auctions,
        totalCount: state.totalCount,
        pageCount: state.pageCount
    }), shallow);

    const setData = useNFTAuctionStore(state => state.setData);

    const setParams = useParamsStore(state => state.setParams);
    const url = qs.stringifyUrl({ url: '', query: params })

    function setPageNumber(pageNumber: number) {
        setParams({ pageNumber })
    }
//this allows us to do a side effect when the listing component first loads.
//and then depending on what happens, on what we're using inside this use effect
//it may cause the component to rerender based on the code inside here.

    useEffect(() => {
        getData(url).then(data => {
            setData(data);
            setLoading(false);
        })
    }, [url, setData]) // whenever the url changes, use effect gets called //we are adding here dependencies
    // if we don't have any dependencies then we would use an empty array, says use effect to run once

    if (loading) return <h3>Loading...</h3>

    return (
        <>
            <Filters />
            {data.totalCount === 0 ? (
                <EmptyFilter showReset />
            ) : (
                <>
                    <div className='grid grid-cols-4 gap-6'>
                    {data && data.nftAuctions ? (
                    data.nftAuctions.map(auction => (
                        <NFTAuctionCard nftauction={auction} key={auction.id} />
                    ))
                    ) : (
                            <p>No auctions available.</p>
                        )}
                    </div>
                    <div className='flex justify-center mt-4'>
                        <AppPagination pageChanged={setPageNumber}
                            currentPage={params.pageNumber} pageCount={data.pageCount} />
                    </div>
                </>
            )}

        </>

    )
}