import React, { useState, useEffect } from 'react';

// Mock data for cascading dropdowns. In a real application,
// this data would be fetched from an API.
const mockCities = [
     { id: 'hanoi', name: 'Hà Nội' },
     { id: 'hcm', name: 'Hồ Chí Minh' },
     { id: 'danang', name: 'Đà Nẵng' },
];

interface District {
     id: string;
     name: string;
}

const mockDistricts: Record<string, District[]> = {
     hanoi: [
          { id: 'hoankiem', name: 'Hoàn Kiếm' },
          { id: 'dongda', name: 'Đống Đa' },
          { id: 'caugiay', name: 'Cầu Giấy' },
     ],
     hcm: [
          { id: 'q1', name: 'Quận 1' },
          { id: 'q3', name: 'Quận 3' },
          { id: 'binhthanh', name: 'Bình Thạnh' },
     ],
     danang: [
          { id: 'haichau', name: 'Hải Châu' },
          { id: 'sona', name: 'Sơn Trà' },
     ],
};

interface Ward {
     id: string;
     name: string;
}

const mockWards: Record<string, Ward[]> = {
     hoankiem: [
          { id: 'phucan', name: 'Phúc An' },
          { id: 'chuongduong', name: 'Chương Dương' },
     ],
     dongda: [
          { id: 'langha', name: 'Láng Hạ' },
          { id: 'thinhquang', name: 'Thịnh Quang' },
     ],
     // ... and so on for other districts
};

// Main UserPage component
export default function UserPage() {
     // State for all form fields
     const [formData, setFormData] = useState({
          'user_info_name': '',
          'user_info_email': '',
          'user_info_phone': '',
          'user_info_birthday': '',
          'user_info_gender': '',
          'user_info_city': '',
          'user_info_district': '',
          'user_info_ward': '',
          'user_info_street': '',
          'user_info_house_number': '',
          'user_info_additional_info': ''
     });

     // State for avatar management
     const [avatarUrl, setAvatarUrl] = useState('https://placehold.co/150x150/E2E8F0/64748B?text=AVATAR');

     // State for cascading address dropdowns
     const [districts, setDistricts] = useState<District[]>([]);
     const [wards, setWards] = useState<Ward[]>([]);

     // Function to handle form input changes
     const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
          const { name, value } = e.target;
          setFormData(prev => ({ ...prev, [name]: value }));
     };

     // Effect to update districts when city changes
     useEffect(() => {
          if (formData.user_info_city && mockDistricts[formData.user_info_city]) {
               setDistricts(mockDistricts[formData.user_info_city]);
          } else {
               setDistricts([]);
               setFormData(prev => ({ ...prev, user_info_district: '', user_info_ward: '' }));
          }
     }, [formData.user_info_city]);

     // Effect to update wards when district changes
     useEffect(() => {
          if (formData.user_info_district && mockWards[formData.user_info_district]) {
               setWards(mockWards[formData.user_info_district]);
          } else {
               setWards([]);
               setFormData(prev => ({ ...prev, user_info_ward: '' }));
          }
     }, [formData.user_info_district]);

     // Function to handle file upload (for avatar)
     const handleAvatarUpload = (e: React.ChangeEvent<HTMLInputElement>) => {
          if (e.target.files && e.target.files[0]) {
               const file = e.target.files[0];
               const newUrl = URL.createObjectURL(file);
               setAvatarUrl(newUrl);
          }
     };

     // Function to handle form submission
     const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
          e.preventDefault();
          console.log('Form Submitted!', formData);
          // Here you would typically send the data to an API
          alert('Thông tin đã được cập nhật thành công!');
     };

     return (
          <div className="mx-auto flex max-w-[75rem] flex-col items-center justify-center p-4 lg:px-0" id='user-info-container'>
               {/* Main container centered with max-width */}
               <div className="mx-auto w-full rounded-lg bg-white p-6 shadow-lg sm:p-10 lg:p-12">
                    <h1 className="mb-10 text-center text-3xl font-bold text-gray-800 sm:text-4xl">Cập nhật thông tin cá nhân</h1>

                    <form onSubmit={handleSubmit} className="space-y-8">
                         {/* Avatar and Basic Info section */}
                         <div className="flex flex-col items-center space-y-6 rounded-xl bg-gray-100 p-6 shadow-inner md:flex-row md:space-y-0 md:space-x-12" id='user-info-basic'>
                              <div className="relative flex-shrink-0">
                                   <img
                                        src={avatarUrl}
                                        alt="User Avatar"
                                        className="h-32 w-32 rounded-full object-cover ring-4 ring-blue-400 ring-offset-4 ring-offset-gray-100"
                                   />
                                   <label htmlFor="user-info-avatar-upload" className="bg-main-color absolute right-0 bottom-0 cursor-pointer rounded-full p-2 shadow-lg transition-colors">
                                        <svg className="h-5 w-5 text-white" fill="currentColor" viewBox="0 0 20 20">
                                             <path d="M13.586 3.586a2 2 0 112.828 2.828l-.793.793-2.828-2.828.793-.793zm-7.586 11.414l-4 4V17l4-4 4-4 4 4-4 4zm-1-6l-2-2 2-2 2 2-2 2zm-2-2l-2-2 2-2 2 2-2 2z"></path>
                                        </svg>
                                        <input
                                             id="user-info-avatar-upload"
                                             type="file"
                                             accept="image/*"
                                             onChange={handleAvatarUpload}
                                             className="hidden"
                                        />
                                   </label>
                              </div>

                              <div className="w-full flex-1 space-y-4">
                                   <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
                                        <div>
                                             <label htmlFor="user-info-name" className="block text-sm font-medium text-gray-700">Tên người dùng</label>
                                             <input
                                                  type="text"
                                                  id="user-info-name"
                                                  name="user_info_name"
                                                  value={formData.user_info_name}
                                                  onChange={handleChange}
                                                  className="mt-1 block w-full rounded-md border border-gray-300 bg-white px-3 py-2 shadow-sm transition-colors focus:border-transparent focus:ring-0 focus:outline-none"
                                             />
                                        </div>
                                        <div>
                                             <label htmlFor="user-info-email" className="block text-sm font-medium text-gray-700">Email</label>
                                             <input
                                                  type="email"
                                                  id="user-info-email"
                                                  name="user_info_email"
                                                  value={formData.user_info_email}
                                                  onChange={handleChange}
                                                  className="mt-1 block w-full rounded-md border border-gray-300 bg-white px-3 py-2 shadow-sm transition-colors focus:border-transparent focus:ring-0 focus:outline-none"
                                             />
                                        </div>
                                   </div>

                                   <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
                                        <div>
                                             <label htmlFor="user-info-phone" className="block text-sm font-medium text-gray-700">Số điện thoại</label>
                                             <input
                                                  type="tel"
                                                  id="user-info-phone"
                                                  name="user_info_phone"
                                                  value={formData.user_info_phone}
                                                  onChange={handleChange}
                                                  className="mt-1 block w-full rounded-md border border-gray-300 bg-white px-3 py-2 shadow-sm transition-colors focus:border-transparent focus:ring-0 focus:outline-none"
                                             />
                                        </div>
                                        <div>
                                             <label htmlFor="user-info-birthday" className="block text-sm font-medium text-gray-700">Ngày sinh</label>
                                             <div className="relative">
                                                  <input
                                                       type="date"
                                                       id="user-info-birthday"
                                                       name="user_info_birthday"
                                                       value={formData.user_info_birthday}
                                                       onChange={handleChange}
                                                       className="mt-1 block w-full appearance-none rounded-md border border-gray-300 bg-white px-3 py-2 shadow-sm transition-colors focus:border-transparent focus:ring-0 focus:outline-none"
                                                  />
                                                  <div className="pointer-events-none absolute inset-y-0 right-0 flex items-center pr-3">
                                                       <svg className="h-5 w-5 text-gray-400" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor">
                                                            <path d="M6 2a1 1 0 00-1 1v1H4a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V6a2 2 0 00-2-2h-1V3a1 1 0 10-2 0v1H7V3a1 1 0 00-1-1zm0 5a1 1 0 000 2h8a1 1 0 100-2H6z" />
                                                       </svg>
                                                  </div>
                                             </div>
                                        </div>
                                   </div>

                                   <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
                                        <div>
                                             <label htmlFor="user-info-gender" className="block text-sm font-medium text-gray-700">Giới tính</label>
                                             <select
                                                  id="user-info-gender"
                                                  name="user_info_gender"
                                                  value={formData.user_info_gender}
                                                  onChange={handleChange}
                                                  className="mt-1 block w-full rounded-md border border-gray-300 bg-white px-3 py-2 shadow-sm transition-colors focus:border-transparent focus:ring-0 focus:outline-none"
                                             >
                                                  <option value="">Chọn giới tính</option>
                                                  <option value="male">Nam</option>
                                                  <option value="female">Nữ</option>
                                                  <option value="other">Khác</option>
                                             </select>
                                        </div>
                                   </div>
                              </div>
                         </div>

                         {/* Address section */}
                         <div className="space-y-4 rounded-xl bg-gray-100 p-6 shadow-inner" id='user-info-address'>
                              <h2 className="text-xl font-semibold text-gray-800">Thông tin địa chỉ</h2>
                              <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 md:grid-cols-4">
                                   <div>
                                        <label htmlFor="user-info-city" className="block text-sm font-medium text-gray-700">Tỉnh/Thành phố</label>
                                        <select
                                             id="user-info-city"
                                             name="user_info_city"
                                             value={formData.user_info_city}
                                             onChange={handleChange}
                                             className="mt-1 block w-full rounded-md border border-gray-300 bg-white px-3 py-2 shadow-sm transition-colors focus:border-transparent focus:ring-0 focus:outline-none"
                                        >
                                             <option value="">Chọn Tỉnh/Thành phố</option>
                                             {mockCities.map(city => (
                                                  <option key={city.id} value={city.id}>{city.name}</option>
                                             ))}
                                        </select>
                                   </div>
                                   <div>
                                        <label htmlFor="user-info-district" className="block text-sm font-medium text-gray-700">Quận/Huyện</label>
                                        <select
                                             id="user-info-district"
                                             name="user_info_district"
                                             value={formData.user_info_district}
                                             onChange={handleChange}
                                             disabled={!formData.user_info_city}
                                             className="mt-1 block w-full rounded-md border border-gray-300 bg-white px-3 py-2 shadow-sm transition-colors focus:border-transparent focus:ring-0 focus:outline-none disabled:opacity-50"
                                        >
                                             <option value="">Chọn Quận/Huyện</option>
                                             {districts.map(district => (
                                                  <option key={district.id} value={district.id}>{district.name}</option>
                                             ))}
                                        </select>
                                   </div>
                                   <div>
                                        <label htmlFor="user-info-ward" className="block text-sm font-medium text-gray-700">Phường/Xã</label>
                                        <select
                                             id="user-info-ward"
                                             name="user_info_ward"
                                             value={formData.user_info_ward}
                                             onChange={handleChange}
                                             disabled={!formData.user_info_district}
                                             className="mt-1 block w-full rounded-md border border-gray-300 bg-white px-3 py-2 shadow-sm transition-colors focus:border-transparent focus:ring-0 focus:outline-none disabled:opacity-50"
                                        >
                                             <option value="">Chọn Phường/Xã</option>
                                             {wards.map(ward => (
                                                  <option key={ward.id} value={ward.id}>{ward.name}</option>
                                             ))}
                                        </select>
                                   </div>
                                   <div>
                                        <label htmlFor="user-info-street" className="block text-sm font-medium text-gray-700">Tên đường</label>
                                        <input
                                             type="text"
                                             id="user-info-street"
                                             name="user_info_street"
                                             value={formData.user_info_street}
                                             onChange={handleChange}
                                             className="mt-1 block w-full rounded-md border border-gray-300 bg-white px-3 py-2 shadow-sm transition-colors focus:border-transparent focus:ring-0 focus:outline-none"
                                        />
                                   </div>
                              </div>

                              <div>
                                   <label htmlFor="user-info-address-number" className="block text-sm font-medium text-gray-700">Số nhà/Tên tòa nhà</label>
                                   <input
                                        type="text"
                                        id="user-info-address-number"
                                        name="user_info_house_number"
                                        value={formData.user_info_house_number}
                                        onChange={handleChange}
                                        className="mt-1 block w-full rounded-md border border-gray-300 bg-white px-3 py-2 shadow-sm transition-colors focus:border-transparent focus:ring-0 focus:outline-none"
                                   />
                              </div>
                              <div>
                                   <label htmlFor="user-info-additional-info" className="block text-sm font-medium text-gray-700">Thông tin bổ sung</label>
                                   <textarea
                                        id="user-info-additional-info"
                                        name="user_info_additional_info"
                                        rows={3}
                                        value={formData.user_info_additional_info}
                                        onChange={handleChange}
                                        className="mt-1 block w-full rounded-md border border-gray-300 bg-white px-3 py-2 shadow-sm transition-colors focus:border-transparent focus:ring-0 focus:outline-none"
                                   />
                              </div>
                         </div>

                         {/* Submit button */}
                         <div className="flex justify-center">
                              <button
                                   type="submit"
                                   className="w-full rounded-full px-8 py-3 font-semibold shadow-lg sm:w-auto"
                              >
                                   Lưu thay đổi
                              </button>
                         </div>
                    </form>
               </div>
          </div>
     );
}
