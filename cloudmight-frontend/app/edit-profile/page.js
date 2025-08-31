"use client"

import Link from 'next/link';
import {useEffect, useState} from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/context/AuthContext";

export default function EditProfilePage(){
    const [user, setUser] = useState(null);
    const [file, setFile] = useState(null);
    const router = useRouter();
    const {token,setToken} = useAuth();
    
    useEffect(()=>{
        if(!token){
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
                console.log("Error fetching user:", error);
            }
        }
        fetchUsers();
    },[router,token,setToken]);

    async function handleSubmit(event){
        event.preventDefault()

        const formData = new FormData(event.currentTarget)
        const payload = new FormData();
        payload.append("Username", formData.get("userName"));
        payload.append("FirstName", formData.get("firstName"));
        payload.append("LastName", formData.get("lastName"));
        payload.append("Email", formData.get("email"));
        if(file && file.size > 0){
            payload.append("ProfilePicture", file);
        }
        const response = await fetch('http://localhost:5245/api/auth/edit-profile', {
            method: 'PUT',
            headers: {
                Authorization: `Bearer ${token}`,
            },
            body: payload,
        })
        if(response.ok){
            alert("Update successful!");
            router.push("/profile");
        } else {
            let errorMessage = "Update failed";
            try {
                const res = await response.text();
                errorMessage = text ? JSON.parse(text).message || text: errorMessage;
            } catch {
                "AAAA"
            }
            alert(errorMessage);
        }
    }
    
    return(
        <div className="flex item-center justify-center mt-40">
            {user ? (
            <form onSubmit={handleSubmit} className="bg-white/40 backdrop-blur-sm p-5 rounded-4xl shadow-xl text-zinc-700 w-125">
                <h2 className="text-center mb-3 font-bold text-2xl pb-7">Edit Profile</h2>
                <input 
                    className="w-full border border-zinc-300 px-4 py-2 mb-4 rounded-4xl shadow-lg" 
                    name="userName" 
                    type="text" 
                    defaultValue={user.userName}/>
                <input 
                    className="w-full border border-zinc-300 px-4 py-2 mb-4 rounded-4xl shadow-lg" 
                    name="firstName" 
                    type="text"
                    defaultValue={user.firstName} />
                <input 
                    className="w-full border border-zinc-300 px-4 py-2 mb-4 rounded-4xl shadow-lg" 
                    name="lastName" 
                    type="text"
                    defaultValue={user.lastName}/>
                <input 
                    className="w-full border border-zinc-300 px-4 py-2 mb-4 rounded-4xl shadow-lg" 
                    name="email" 
                    type="email"
                    defaultValue={user.email} />
                <input 
                    className="w-full border border-zinc-300 px-4 py-2 mb-4 rounded-4xl shadow-lg  h-30" 
                    name="ProfilePicture" 
                    type="file"
                    accept="image/*"
                    onChange={(e) => setFile(e.target.files[0])}
                    defaultValue={user.profilePicUrl}/>
                <button 
                    className="block mx-auto flex items-center justify-center w-30 bg-zinc-700 text-white shadow-lg px-4 py-2 mb-4 rounded-4xl hover:bg-zinc-800" 
                    type="submit">
                    Submit
                </button>
            </form>
                ) : (
                    <p>Loading...</p>
            )}
        </div>
    )
}