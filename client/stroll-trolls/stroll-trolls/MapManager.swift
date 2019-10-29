//
//  MapManager.swift
//  stroll-trolls
//
//  Created by Kayton Fletcher on 10/26/19.
//  Copyright © 2019 Kayton Fletcher. All rights reserved.
//

import Foundation

struct MapManager {
    
    
    func genRoute(){
        let serverUrl = "http://128.61.97.214:3000/api/MapBox"
           
        var url = URLComponents(string: serverUrl)!
        url.queryItems = [
                   URLQueryItem(name: "distance", value: "6"),
                   URLQueryItem(name: "coordinates", value: "30"),
                   URLQueryItem(name: "coordinates", value: "60"),
                   URLQueryItem(name: "coordinates", value: "31"),
                   URLQueryItem(name: "coordinates", value: "60"),
               
               ]
        
        let request = URLRequest(url: url.url!)

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data,                            // is there data
                let response = response as? HTTPURLResponse,  // is there HTTP response
                (200 ..< 300) ~= response.statusCode,         // is statusCode 2XX
                error == nil else {                           // was there no error, otherwise ...
                    
                    return
            }

            let responseObject = (try? JSONSerialization.jsonObject(with: data)) as? [String: Any]
            print(responseObject!)
        }
        task.resume()
        
        
    }
     
}


