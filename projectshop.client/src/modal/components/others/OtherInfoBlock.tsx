import { FaMoneyBillAlt, FaGift, FaShieldAlt, FaPhone } from "react-icons/fa";

export default function OtherInfoBlock() {
    const items = [
        {
            icon: <FaMoneyBillAlt className="text-3xl main-color" />,
            title: "Thanh Toán",
            desc: "Khi Nhận Hàng",
        },
        {
            icon: <FaGift className="text-3xl main-color" />,
            title: "Quà Tặng",
            desc: "Miễn Phí",
        },
        {
            icon: <FaShieldAlt className="text-3xl main-color" />,
            title: "Bảo Mật",
            desc: "Thanh Toán Trực Tuyến",
        },
        {
            icon: <FaPhone className="text-3xl main-color" />,
            title: "Hỗ Trợ",
            desc: "24/7",
        },
    ];

    return (
        <section className="w-full py-2">
            <div className="mx-auto max-w-[1200px]">
                <div className="bg-white/70 border border-blue-100 rounded-lg shadow-sm grid grid-cols-1 md:grid-cols-4 divide-y md:divide-y-0 md:divide-x divide-blue-100">
                    {items.map((item, idx) => (
                        <div
                            key={idx}
                            className="flex items-center gap-3 px-6 py-3"
                        >
                            {item.icon}
                            <div>
                                <div className="font-bold text-sm md:text-base text-gray-700">{item.title}</div>
                                <div className="text-xs md:text-sm text-gray-500">{item.desc}</div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </section>
    );
}