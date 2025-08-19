"use client";
import { useRouter } from "next/navigation";
import { useEffect } from "react";
import Link from "next/link";
import { useAuth } from "@/context/AuthContext";

export default function LoginPage(){
    const router = useRouter();
    const {token,setToken} = useAuth();
    useEffect(() => {
        if (token) {
            router.push("/profile");
        }
    }, [router,token,setToken]);
    async function handleSubmit(event){
        event.preventDefault()
        const formData = new FormData(event.currentTarget)
        const email = formData.get('email')
        const password = formData.get('password')
        const response = await fetch('http://localhost:5245/api/auth/login', {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({email, password}),
        })
        if(response.ok){
            const data = await response.json();
            // localStorage.setItem('token', data.token);
            setToken(data.token);
            router.push('/profile');
        } else {
            const errorData = await response.json();
            alert(errorData.message || "Login failed");
        }
    }
    return (
        <div className="flex item-center justify-center mt-40">
            <form onSubmit={handleSubmit} className="bg-white/40 backdrop-blur-sm p-5 rounded-4xl shadow-xl text-zinc-700 w-125">
                <h2 className="text-center mb-3 font-bold text-2xl">Log In</h2>
                <input className="w-full border border-zinc-300 px-4 py-2 mb-4 rounded-4xl shadow-lg" name="email" type="email" required placeholder="Email" />
                <input className="w-full border border-zinc-300 px-4 py-2 mb-4 rounded-4xl shadow-lg" name="password" type="password" required placeholder="Password" />
                <button className="block mx-auto flex items-center justify-center w-30 bg-zinc-700 text-white  shadow-lg px-4 py-2 mb-4 rounded-4xl hover:bg-zinc-800" type="submit">Submit</button>
                <div className="text-center mb-3 font-bold">
                    <Link href={"/"}>Forgot password?</Link>
                </div>
                <hr/>
                <div className="block flex items-center justify-center px-2 py-2 my-4">
                    <p className="mr-2 text-zinc-700">Don't have an account?</p>
                    <Link className="text-zinc-900" href={"/register"}>Register</Link>
                </div>
            </form>
        </div>
    )
}