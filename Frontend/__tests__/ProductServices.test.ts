import { ProductServices } from '../src/services/ProductServices';
import { Product } from '../src/interfaces/models/Product';
import { jest, describe, it, expect, afterEach } from '@jest/globals';
import { API } from '../src/services/Api';

// Mock localStorage
const localStorageMock = {
    getItem: jest.fn(),
    setItem: jest.fn(),
    removeItem: jest.fn(),
    clear: jest.fn(),
};
global.localStorage = localStorageMock as any;

jest.mock('../src/services/Api');
const mockedAPI = API as jest.Mocked<typeof API>;

// Mock data để test
const MOCK_PRODUCTS: Product[] = [
    { id: 1, name: 'Product 1', price: 100, description: 'Description 1', quantity: 1, img: 'image1.jpg' },
    { id: 2, name: 'Product 2', price: 200, description: 'Description 2', quantity: 1, img: 'image2.jpg' },
    { id: 3, name: 'Product 3', price: 300, description: 'Description 3', quantity: 1, img: 'image3.jpg' },
];

describe('ProductServices', () => {
    // Clear all mocks sau mỗi test
    afterEach(() => {
        jest.clearAllMocks();
        localStorage.clear();
    });

    describe('getAll', () => {
        it('should return all products', async () => {
            // Setup mock response
            mockedAPI.get.mockResolvedValueOnce({ data: MOCK_PRODUCTS });

            // Gọi service
            const result = await ProductServices.getAll();

            // Verify kết quả
            expect(result.data).toEqual(MOCK_PRODUCTS);
            expect(mockedAPI.get).toHaveBeenCalledWith('/products', {"params": {"startWith": undefined}});
            expect(mockedAPI.get).toHaveBeenCalledTimes(1);
        });

        it('should handle error when fetching products', async () => {
            // Setup mock error
            const error = new Error('Network error');
            mockedAPI.get.mockRejectedValueOnce(error);

            // Verify error handling
            await expect(ProductServices.getAll()).rejects.toThrow('Network error');
        });
    });

    describe('getById', () => {
        it('should return product by id', async () => {
            const mockProduct = MOCK_PRODUCTS[0];
            mockedAPI.get.mockResolvedValueOnce({ data: mockProduct });

            const result = await ProductServices.getById('1');

            expect(result.data).toEqual(mockProduct);
            expect(mockedAPI.get).toHaveBeenCalledWith('/products/1');
        });
    });


    // describe('error handling', () => {
    //     it('should handle network errors', async () => {
    //         const networkError = new Error('Network Error');
    //         mockedAPI.get.mockRejectedValueOnce(networkError);

    //         await expect(ProductServices.getAll()).rejects.toThrow('Network Error');
    //     });

    //     it('should handle 404 errors', async () => {
    //         const notFoundError = {
    //             response: { status: 404, data: { message: 'Product not found' } }
    //         };
    //         mockedAPI.get.mockRejectedValueOnce(notFoundError);

    //         await expect(ProductServices.getById('999')).rejects.toThrow('Product not found');
    //     });

    //     it('should handle validation errors', async () => {
    //         const validationError = {
    //             response: { 
    //                 status: 400, 
    //                 data: { message: 'Invalid product data' }
    //             }
    //         };
    //         mockedAPI.post.mockRejectedValueOnce(validationError);

    //         const invalidProduct = {
    //             name: '',
    //             price: -1,
    //             description: '',
    //             quantity: 0,
    //             img: ''
    //         };

    //         await expect(ProductServices.create(invalidProduct)).rejects.toThrow('Invalid product data');
    //     });
    // });
});