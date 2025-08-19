import Link from "next/link";

export default function Contact() {
    return (
        <div className="flex item-center justify-center mt-40">
            <div className="bg-white/40 backdrop-blur-sm p-5 rounded-4xl shadow-xl text-zinc-700 w-200 mb-7">
                <h2 className="text-center font-bold p-5 border-b border-zinc-700">My Account</h2>
                <div className="grid grid-cols-1 md:grid-cols-2 p-5">
                    <div>
                        <div className="px-3">
                            <p className="font-semibold mt-2">Username:</p>
                            <p className="mb-2">user</p>
                        </div>
                        <div className="border-t border-zinc-700 px-3">
                            <p className="font-semibold mt-2">First Name:</p>
                            <p className="mb-2">user</p>
                        </div>
                    </div>
                    <div className="flex items-center justify-center pt-5">
                        <figure>
                            <img src={"/cards/card4.jpg"} alt="Profile Photo" className="w-60 h-60 object-cover rounded-full"/>
                        </figure>
                    </div>
                </div>
            </div>
        </div>
    );
}