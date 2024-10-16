import axios from 'axios';
import { AxiosResponse } from 'axios';

export class WebApi {
    static config = {
        baseURL: process.env.BASE_URL + 'api',
        timeout: 100000,
        headers: {
            'Content-Type': 'application/json',
        },
    };
    static api = axios.create(WebApi.config);

    public static post<T>(url: string, data: any, config?: any): Promise<AxiosResponse<T>> {
        return WebApi.api.post<any, AxiosResponse<T, any>, any>(url, data, config);
    }

    public static get<T>(url: string, config?: any) {
        return WebApi.api.get<any, AxiosResponse<T, any>, any>(url, config);
    }

    public static put<T>(url: string, data: any, config?: any): Promise<AxiosResponse<T>> {
        return WebApi.api.put<any, AxiosResponse<T, any>, any>(url, data, config);
    }

    public static delete<T>(url: string, config?: any): Promise<AxiosResponse<T>> {
        return WebApi.api.delete<any, AxiosResponse<T, any>, any>(url, config);
    }

}