import Link from 'next/link';

export default function HomePage(){
    return (
        <div className=" w-full p-10 gap-5 grid grid-cols-1 ">
            <div className="relative w-full h-screen bg-[url('/mountains1.jpg')] bg-cover bg-fixed">
                <div className="absolute inset-0 bg-black/30 flex flex-col items-center justify-center p-6 text-center">
                    <h1 className="text-5xl sm:text-6xl font-bold text-white mb-6">
                        CloudMight
                    </h1>
                    <p className="text-xl sm:text-2xl text-white/80 max-w-2xl mb-8">
                        High-speed, secure, and cross-platform cloud storage. Instantly mount your partitions with no client-side configuration required.
                    </p>
                    <div className="flex gap-4 justify-center">
                        <Link href="/register" className="px-6 py-3 rounded-3xl bg-white/70 backdrop-blur-sm text-zinc-700 font-semibold hover:bg-white/50 transition">
                            Get Started
                        </Link>
                        <Link href="/about" className="px-6 py-3 rounded-3xl border border-white/40 text-white/90 font-semibold hover:bg-white/20 transition">
                            Learn More
                        </Link>
                    </div>
                </div>
            </div>
            <div className="w-full gap-5 shadow-xl bg-white/40 backdrop-blur-sm p-5 text-zinc-700 rounded-4xl items-center justify-items-center align-center min-h-20">
                <div className="text-3xl">Server status: Currently Offline</div>
            </div>
            <div className="w-full grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-5">
                <div className="card w-full shadow-xl bg-white/40 backdrop-blur-sm text-zinc-700 rounded-4xl items-center justify-items-center align-center hover:-translate-y-3 hover:scale-105 hover:text-zinc-600 transition-transform duration-300 ease-in-out hover:bg-white/30">
                    <figure>
                        <img src={"/cards/card2.jpg"} alt="Mounting" className="min-h-85"/>
                    </figure>
                    <div className="card-body w-full">
                        <div className="card-title">Instant mounting</div>
                        <p className="text-lg">Our automated script handles the mounting process entirely on the server side, requiring no manual configuration from the client.</p>
                        <div className="card-actions justify-center border-t border-zinc-700 font-bold">
                            <Link href="/about">Read more</Link>
                        </div>
                    </div>
                </div>
                <div className="card w-full shadow-xl bg-white/40 backdrop-blur-sm text-zinc-700 rounded-4xl items-center justify-items-center align-center hover:-translate-y-3 hover:scale-105 hover:text-zinc-600 transition-transform duration-300 ease-in-out hover:bg-white/30">
                    <figure>
                        <img src={"/cards/card3.jpg"} alt="Mounting" className="min-h-85" />
                    </figure>
                    <div className="card-body w-full">
                        <div className="card-title">High Speed</div>
                        <p className="text-lg">Connections are routed through OSI Layer 3, ensuring direct and efficient communication for maximum speed.</p>
                        <div className="card-actions justify-center border-t border-zinc-700 font-bold">
                            <Link href="/about">Read more</Link>
                        </div>
                    </div>
                </div>
                <div className="card w-full shadow-xl bg-white/40 backdrop-blur-sm text-zinc-700 rounded-4xl items-center justify-items-center align-center hover:-translate-y-3 hover:scale-105 hover:text-zinc-600 transition-transform duration-300 ease-in-out hover:bg-white/30">
                    <figure>
                        <img
                            src={"/cards/card1.jpg"}
                            alt="Mounting" className="min-h-85"/>
                    </figure>
                    <div className="card-body w-full">
                        <div className="card-title">Secure Encryption</div>
                        <p className="text-lg">All traffic is encrypted using WireGuard, providing a secure and private connection at all times..</p>
                        <div className="card-actions justify-center border-t border-zinc-700 font-bold">
                            <Link href="/about">Read more</Link>
                        </div>
                    </div>
                </div>
                <div className="card w-full shadow-xl bg-white/40 backdrop-blur-sm text-zinc-700 rounded-4xl items-center justify-items-center align-center hover:-translate-y-3 hover:scale-105 hover:text-zinc-600 transition-transform duration-300 ease-in-out hover:bg-white/30">
                    <figure>
                        <img
                            src={"/cards/card4.jpg"} alt="Mounting" className="min-h-85"/>
                    </figure>
                    <div className="card-body w-full">
                        <div className="card-title">Cross-Platform</div>
                        <p className="text-lg">Our service supports both Windows and Linux environments, enabling seamless use across different systems.</p>
                        <div className="card-actions justify-center border-t border-zinc-700 font-bold">
                            <Link href="/about">Read more</Link>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>
    );
}