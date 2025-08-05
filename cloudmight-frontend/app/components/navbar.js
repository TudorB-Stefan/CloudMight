 import {useEffect, useState} from "react";
import Link from "next/link";
export default function Navbar() {
    const [user,setUser] = useState(null);
    useEffect(() => {
        async function fetchUser() {
            try {
                const res = await fetch("http://localhost:5245/api/User/user-info",{
                    credentials: "include",
                });
                if(!res.ok) return ;
                const data = await res.json();
                setUser(data);
            } catch (err) {
                console.error("Failed to fetch user",err);
            }
        }
        fetchUser();
    },[]);
    return (
        <>
            <div className="navbar bg-red-900 shadow-sm rounded-4xl px-6 mb-10">
                <div className="navbar-start">
                    <div className="dropdown">
                        <div tabIndex="0" role="button" className="btn btn-ghost btn-circle">
                            <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24"
                                 stroke="currentColor">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M4 6h16M4 12h16M4 18h7"/>
                            </svg>
                        </div>
                        <ul tabIndex="0"
                            className="menu menu-sm dropdown-content bg-base-100 rounded-box z-1 mt-3 w-52 p-2 shadow">
                            <li><Link href="/">Homepage</Link></li>
                            <li><Link href="/about">About</Link></li>
                        </ul>
                    </div>
                </div>
                <div className="navbar-center">
                    <Link href="" className="btn btn-ghost text-xl">CloudMight</Link>
                </div>
                <div className="navbar-end gap-5">
                    <button>
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none"
                             stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"
                             className="lucide lucide-search-icon lucide-search">
                            <path d="m21 21-4.34-4.34"/>
                            <circle cx="11" cy="11" r="8"/>
                        </svg>
                    </button>
                    <button>
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none"
                             stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"
                             className="lucide lucide-bell-icon lucide-bell">
                            <path d="M10.268 21a2 2 0 0 0 3.464 0"/>
                            <path
                                d="M3.262 15.326A1 1 0 0 0 4 17h16a1 1 0 0 0 .74-1.673C19.41 13.956 18 12.499 18 8A6 6 0 0 0 6 8c0 4.499-1.411 5.956-2.738 7.326"/>
                        </svg>
                    </button>
                    {user ? <Link href={""}> `${user.username}`</Link> : <Link href={""}>Log In</Link>}
                </div>
            </div>
        </>
    );
}