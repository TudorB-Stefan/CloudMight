"use client";
import { useRouter } from "next/navigation";
import Link from "next/link";
import {useEffect} from "react";

export default function LoginPage(){
    const router = useRouter();
    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            router.push("/profile");
        }
    }, [router]);
    async function handleSubmit(event){
        event.preventDefault()
        
        const formData = new FormData(event.currentTarget)
        const payload = {
            username: formData.get('userName'),
            firstName: formData.get('firstName'),
            lastName: formData.get('lastName'),
            email: formData.get('email'),
            password: formData.get('password'),
            confirmPassword: formData.get('confirmPassword')
        };
        const response = await fetch('http://localhost:5245/api/auth/register', {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(payload),
        })
        if(response.ok){
            alert("Registration successful!");
            router.push("/login");
        } else {
            const errorData = await response.json();
            alert(response.message || "Registration failed");
        }
    }
    return (
        <div className="flex item-center justify-center mt-40">
            <form onSubmit={handleSubmit} className="bg-white/40 backdrop-blur-sm p-5 rounded-4xl shadow-xl text-zinc-700 w-125">
                <h2 className="text-center mb-3 font-bold text-2xl">Register</h2>
                <input className="w-full border border-zinc-300 px-4 py-2 mb-4 rounded-4xl shadow-lg" name="userName" type="text" placeholder="Username" />
                <input className="w-full border border-zinc-300 px-4 py-2 mb-4 rounded-4xl shadow-lg" name="firstName" type="text" placeholder="First Name (optional)" />
                <input className="w-full border border-zinc-300 px-4 py-2 mb-4 rounded-4xl shadow-lg" name="lastName" type="text" placeholder="Last Name (optional)" />
                <input className="w-full border border-zinc-300 px-4 py-2 mb-4 rounded-4xl shadow-lg" name="email" type="email" placeholder="Email" />
                <input className="w-full border border-zinc-300 px-4 py-2 mb-4 rounded-4xl shadow-lg" name="password" type="password" placeholder="Password" />
                <input className="w-full border border-zinc-300 px-4 py-2 mb-4 rounded-4xl shadow-lg" name="confirmPassword" type="password" placeholder="Confirm Password" />
                <button className="block mx-auto flex items-center justify-center w-30 bg-zinc-700 text-white  shadow-lg px-4 py-2 mb-4 rounded-4xl hover:bg-zinc-800" type="submit">Submit</button>
                <hr/>
                <div className="block flex items-center justify-center px-2 py-2 my-4">
                    <p className="mr-2 text-zinc-700">Already have an account?</p>
                    <Link className="text-zinc-900" href={"/login"}>Log In</Link>
                </div>
            </form>
        </div>
    )
}