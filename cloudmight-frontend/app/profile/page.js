"use client";
import Link from 'next/link';
import {useEffect, useState} from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/context/AuthContext";


export default function Profile(){
    const [user, setUser] = useState(null);
    const router = useRouter();
    const {token,setToken} = useAuth();
    useEffect(() => {
        if (!token) {
            router.push("/login");
            return;
        }
        async function fetchUsers(){
            try {
                const res = await fetch("http://localhost:5245/api/auth/user-info", {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });
                if(!res.ok){
                    console.log("User not found");
                    setToken(null);
                    router.push("/login");
                    return;
                }
                const data = await res.json();
                setUser(data);
            } catch (error) {
                console.log("Error fetching user:", err);
                setToken(null);
                router.push("/login");
            } 
        }
        fetchUsers();
    },[router,token,setToken]);
    function handleLogout() {
        setToken(null);
        router.push("/login");
    }
    if(!user){
        return (
            <div className="flex items-center justify-center mt-40">
                 <p className="text-lg font-semibold">User not found!</p>
             </div>
        );
    }
    return (
        <div className="flex item-center justify-center mt-40">
             <div className="bg-white/40 backdrop-blur-sm p-5 rounded-4xl shadow-xl text-zinc-700 w-200 mb-7">
                 <h2 className="text-center font-bold p-5 border-b border-zinc-700">My Account</h2>
                 <div className="grid grid-cols-1 md:grid-cols-2 p-5 gap-5">
                     <div>
                         <div className="px-3">
                             <p className="font-semibold mt-2">Username:</p>
                             <p className="mb-2">{user.userName}</p>
                         </div>
                         <div className="border-t border-zinc-700 px-3">
                             <p className="font-semibold mt-2">First Name:</p>
                             <p className="mb-2">{user.firstName}</p>
                         </div>
                         <div className="border-t border-zinc-700 px-3">
                             <p className="font-semibold mt-2">Last Name:</p>
                             <p className="mb-2">{user.lastName}</p>
                         </div>
                         <div className="border-t border-zinc-700 px-3 mb-7">
                             <p className="font-semibold mt-2">Email:</p>
                             <p className="mb-2">{user.email}</p>
                         </div>
                         <div className="grid grid-cols-1 md:grid-cols-2 p-5 gap-5">
                             <Link href={"/edit-profile"} className="text-zinc-200 bg-zinc-700 rounded-4xl shadow-lg p-3 hover:bg-zinc-800 hover:text-zinc-300 hover:shadow-xl text-center" >Edit Profile</Link>
                             <button onClick={handleLogout} className="text-zinc-200 bg-zinc-700 rounded-4xl shadow-lg p-3 hover:bg-zinc-800 hover:text-zinc-300 hover:shadow-xl">Log out</button>
                         </div>
                     </div>
                     <div className="grid grid-cols-1">
                         <div className="flex pt-9 justify-center">
                             <Link href={`http://localhost:5245${user.profilePictureUrl}`} target="_blank" rel="noopener noreferrer" className="rounded-full">
                                 <img 
                                     src={`http://localhost:5245${user.profilePictureUrl}`}
                                     onError={(e) => e.currentTarget.src = 'http://localhost:5245/images/default-avatar.jpg'} 
                                     alt="Loading..." className="w-60 h-60 object-cover rounded-full border flex justify-center items-center border-zinc-400 shadow-lg hover:shadow-xl"/>
                             </Link>
                         </div>
                     </div>
                 </div>
             </div>
         </div>
     );
}