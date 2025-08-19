import Link from "next/link";

export default function AboutUs() {
    return (
        <div className="flex item-center justify-center mt-40">
            <div className="bg-white/40 backdrop-blur-sm p-5 rounded-4xl shadow-xl text-zinc-700 w-200 mb-8">
                <p className="text-center font-bold p-5 border-b border-zinc-700 text-xl">About us</p>
                <div className="mb-5 p-5">
                    <p className="font-bold text-lg">Our Mission:</p>
                    <p className="text-lg">Our mission is simple: Empower you to take control of your digital world. 
                        Whether you’re an individual user, a small team, or a growing organization, CloudMight is here to help you keep your data safe, accessible, and entirely yours.
                    </p>
                </div>
                <div className="mb-5 p-5">
                    <p className="font-bold text-lg">Our Story:</p>
                    <p className="text-lg">
                        Founded with the goal of combining innovation and simplicity,
                        CloudMight was developed by passionate technology enthusiasts who wanted to make secure digital storage as easy to use as any everyday app,
                        while still providing the flexibility and control that developers and advanced users demand.
                    </p>
                </div>
                <div className="mb-5 p-5">
                    <p className="font-bold text-lg">Core Values:</p>
                    <p className="text-lg">We take pride in our transparent approach, open communication, and dedication to continuous improvement. 
                        Every feature we release is designed with our community in mind, ensuring that your feedback directly shapes the future of our platform.</p>
                </div>
                
            </div>
        </div>
    );
}