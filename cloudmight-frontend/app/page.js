import Link from 'next/link';

export default function HomePage(){
    return (
        <div className="flex justify-center w-full p-10">
            <div
                className="w-full gap-5 shadow-xl bg-red-900 p-5 text-white rounded-4xl items-center justify-items-center align-center">
                <div className="text-3xl">Server status: ?</div>
            </div>
        </div>
    );
}