import {useEffect, useState} from "react";
import Link from "next/link";
import { useAuth } from "@/context/AuthContext";
export default function Navbar() {
    const [user,setUser] = useState(null);
    const {token,setToken} = useAuth();
    useEffect(() => {
        async function fetchUser() {
            // const token = localStorage.getItem("token");
            if (!token) {
                setUser(null);
                return;
            }
            try {
                const res = await fetch("http://localhost:5245/api/auth/user-info",{
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });
                if(!res.ok) return ;
                const data = await res.json();
                setUser(data);
            } catch (err) {
                console.error("Failed to fetch user",err);
            }
        }
        fetchUser();
    },[token]);
    return (
        <>
            <div className="navbar bg-white/40 backdrop-blur-sm shadow-xl rounded-4xl px-6 mb-10 text-zinc-700 relative z-10">
                <div className="navbar-start">
                    <div className="drawer">
                        <input id="my-drawer" type="checkbox" className="drawer-toggle"/>
                        <div className="drawer-content">
                            {/*<label htmlFor="my-drawer" className="btn btn-primary drawer-button">Open drawer</label>*/}
                            <label tabIndex="0" role="button" htmlFor="my-drawer" className="btn bg-white/0 border-0 shadow-[0] text-zinc-700 hover:bg-zinc-700 hover:text-white hover:shadow-lg btn-circle drawer-button">
                                <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24"
                                     stroke="currentColor">
                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M4 6h16M4 12h16M4 18h7"/>
                                </svg>
                            </label>
                        </div>
                    </div>
                </div>
                {/*<div className="navbar-center">*/}
                {/*    <Link href="/" className="text-xl font-bold  hover:text-zinc-500">CloudMight</Link>*/}
                {/*</div>*/}
                <div className="navbar-end gap-5 font-bold">
                    {user ? (
                            <>
                                <Link href={"/folders"} className="hover:text-zinc-500"> Storage</Link>
                                <Link href={"/profile"} className="hover:text-zinc-500"> My Account</Link>
                            </>
                        ) : (
                            <>
                                <Link href={"/register"} className="hover:text-zinc-500">Register</Link>
                                <Link href={"/login"} className="hover:text-zinc-500">Log In</Link>
                            </>
                        )
                    }
                </div>
            </div>
        </>
    );
}