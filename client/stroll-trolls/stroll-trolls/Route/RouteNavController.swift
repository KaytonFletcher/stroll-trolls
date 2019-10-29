//
//  RouteNav.swift
//  stroll-trolls
//
//  Created by Kayton Fletcher on 10/27/19.
//  Copyright © 2019 Kayton Fletcher. All rights reserved.
//

import UIKit
import Foundation
import CoreLocation
import MapboxCoreNavigation
import MapboxNavigation
import MapboxDirections

class RouteNavController: UIViewController, CLLocationManagerDelegate {
   // let locationManager = CLLocationManager()
    
    var data: [RouteData]?
    

    init(coordinates: [RouteData]) {
        self.data = coordinates
        super.init(nibName: nil, bundle: nil)
    }
    
    required init?(coder aDecoder: NSCoder) {
           super.init(coder: aDecoder)
    }
 
    
    override func viewDidLoad() {
        
        print("FUCKKKKK")
        
        if(data != nil){
            
            var coordinates: [CLLocationCoordinate2D] = []
            var index: Int = 0
            for coord in data! {
                coordinates[index] = CLLocationCoordinate2DMake(coord.item1, coord.item2)
                index+=1
            }
        
            let options = NavigationRouteOptions(coordinates: coordinates)

            Directions.shared.calculate(options) { (waypoints, routes, error) in
            guard let route = routes?.first, error == nil else {
                print(error!.localizedDescription)
            return
            }

            // For demonstration purposes, simulate locations if the Simulate Navigation option is on.
            let navigationService = MapboxNavigationService(route: route, simulating: .onPoorGPS)
            let navigationOptions = NavigationOptions(navigationService: navigationService)
            let navigationViewController = NavigationViewController(for: route, options: navigationOptions)
            navigationViewController.modalPresentationStyle = .fullScreen

            self.present(navigationViewController, animated: true, completion: nil)
            }
        }
    }
}
